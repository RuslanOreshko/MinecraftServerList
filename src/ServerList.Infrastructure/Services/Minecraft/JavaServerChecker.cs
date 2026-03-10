using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ServerList.Application.Abstractions.Services;
using ServerList.Application.Common.Models;

namespace ServerList.Infrastructure.Services.Minecraft;


public sealed class JavaServerChecker : IMinecraftServerCheker
{
    public async Task<ServerChekerResult> CheckAsync(string ip, int port, CancellationToken ct)
    {
        try
        {
            using var client = new TcpClient();

            await client.ConnectAsync(ip, port, ct);

            await using var stream = client.GetStream();

            await SendHandshakeAsync(stream, ip, port, ct);
            await SendStatusRequestAsync(stream, ct);

            var json = await ReadStatusResponseAsync(stream, ct);
            var result = ParseStatusResponse(json);

            return result;
        }
        catch
        {
            return new ServerChekerResult(
                IsOnline: false,
                OnlinePlayers: 0
            );
        }
    }

    private static async Task SendHandshakeAsync(NetworkStream stream, string ip, int port, CancellationToken ct)
    {
        using var ms = new MemoryStream();

        WriteVarInt(ms, 0x00);
        WriteVarInt(ms, -1);
        WriteString(ms, ip);
        WriteUsingShort(ms, (ushort)port);
        WriteVarInt(ms, 1);

        var handashakeData = ms.ToArray();

        using var packet = new MemoryStream();
        WriteVarInt(packet, handashakeData.Length);
        packet.Write(handashakeData, 0, handashakeData.Length);

        var bytes = packet.ToArray();
        await stream.WriteAsync(bytes, ct);
    }

    private static async Task SendStatusRequestAsync(NetworkStream stream, CancellationToken ct)
    {
        var packet = new byte[] { 0x01, 0x00};
        await stream.WriteAsync(packet, ct);
    }

    private static async Task<string> ReadStatusResponseAsync(NetworkStream stream, CancellationToken ct)
    {
        var packetLength = await ReadVarIntAsync(stream, ct);

        if(packetLength <= 0)
            throw new IOException("Invalid packet length.");

        var packetId = await ReadVarIntAsync(stream, ct);
        if(packetId != 0x00)
            throw new IOException("Invalid repsonse packet id.");

        var json = await ReadStringAsync(stream, ct);
        return json;
    }

    private static ServerChekerResult ParseStatusResponse(string json)
    {
        using var document = JsonDocument.Parse(json);

        var root = document.RootElement;

        if (!root.TryGetProperty("players", out var playersElement))
        throw new IOException("Players section not found in server response.");

        if (!playersElement.TryGetProperty("online", out var onlineElement))
            throw new IOException("Online players field not found in server response.");

        var onlinePlayers = onlineElement.GetInt32();

        return new ServerChekerResult(
            IsOnline: true,
            OnlinePlayers: onlinePlayers
        );
    }



    private static void WriteUsingShort(Stream stream, ushort value)
    {
        stream.WriteByte((byte)(value >> 8));
        stream.WriteByte((byte)value);
    }

    private static void WriteString(Stream stream, string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        WriteVarInt(stream, bytes.Length);
        stream.Write(bytes, 0, bytes.Length);
    }

    private static void WriteVarInt(Stream stream, int value)
    {
        uint unsignedValue = (uint)value;

        do
        {
            byte temp = (byte)(unsignedValue & 0b0111_1111);

            unsignedValue >>= 7;

            if(unsignedValue != 0)
                temp |= 0b1000_0000;

            stream.WriteByte(temp);
        }
        while(unsignedValue != 0);
    }


    private static async Task<int> ReadVarIntAsync(Stream stream, CancellationToken ct)
    {
        int numRead = 0;
        int result = 0;
        byte[] buffer = new byte[1];

        while (true)
        {
            var read = await stream.ReadAsync(buffer, 0, 1, ct);
            if (read == 0)
                throw new EndOfStreamException("Stream ended while reading VarInt.");

            byte value = buffer[0];
            int segment = value & 0b0111_1111;
            result |= segment << (7 * numRead);

            numRead++;

            if (numRead > 5)
                throw new IOException("VarInt is too big.");

            if ((value & 0b1000_0000) == 0)
                break;
        }

        return result;
    }

    private static async Task<byte[]> ReadExactAsync(Stream stream, int length, CancellationToken ct)
    {
        var buffer = new byte[length];
        int offset = 0;

        while (offset < length)
        {
            var read = await stream.ReadAsync(buffer, offset, length - offset, ct);
            if (read == 0)
                throw new EndOfStreamException("Stream ended before enough bytes were read.");

            offset += read;
        }

        return buffer;
    }

    private static async Task<string> ReadStringAsync(Stream stream, CancellationToken ct)
    {
        var length = await ReadVarIntAsync(stream, ct);

        if (length < 0)
            throw new IOException("Invalid string length.");

        var bytes = await ReadExactAsync(stream, length, ct);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}