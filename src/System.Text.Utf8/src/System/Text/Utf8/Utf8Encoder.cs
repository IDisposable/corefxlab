﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace System.Text.Utf8
{
    public static class Utf8Encoder
    {
        public static int CharToUtf8(char character, ref FourBytes utf8)
        {
            var codepoint = (ushort)character;
            if (codepoint <= 0x7f)
            {
                utf8.B0 = (byte)codepoint;
                return 1;
            }

            if (codepoint <= 0x7ff)
            {
                utf8.B0 = (byte)(0xC0 | ((codepoint & 0x07C0) >> 6));
                utf8.B1 = (byte)(0x80 | (codepoint & 0x3F));
                return 2;
            }

            if (!char.IsSurrogate(character))
            {
                utf8.B0 = (byte)(0xE0 | ((codepoint & 0xF000) >> 12));
                utf8.B1 = (byte)(0x80 | ((codepoint & 0x0FC0) >> 6));
                utf8.B2 = (byte)(0x80 | (codepoint & 0x003F));
                return 3;
            }

            utf8.B3 = 0;
            throw new NotImplementedException();
        }

        public static char Utf8ToChar(ref FourBytes utf8)
        {
            throw new NotImplementedException();
        }
    }

    // TODO: should this be in System.Buffers?
    public struct FourBytes
    {
        public byte B0;
        public byte B1;
        public byte B2;
        public byte B3;
    }
}
