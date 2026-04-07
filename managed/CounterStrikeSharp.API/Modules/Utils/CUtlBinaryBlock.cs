/*
 *  This file is part of CounterStrikeSharp.
 *  CounterStrikeSharp is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  CounterStrikeSharp is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with CounterStrikeSharp.  If not, see <https://www.gnu.org/licenses/>. *
 */

using System;
using System.Runtime.CompilerServices;

namespace CounterStrikeSharp.API.Modules.Utils
{
    public class CUtlBinaryBlock : NativeObject
    {
        private const int ExternalConstBufferMarker = 1 << 30;
        private const int ExternalBufferMarker = 1 << 31;

        public CUtlBinaryBlock(IntPtr pointer) : base(pointer)
        {
        }

        public unsafe IntPtr Data => Unsafe.Read<IntPtr>((void*)Handle);

        public unsafe ref int AllocationCount => ref Unsafe.AsRef<int>((void*)(Handle + IntPtr.Size));

        public unsafe ref int GrowSize => ref Unsafe.AsRef<int>((void*)(Handle + IntPtr.Size + sizeof(int)));

        public unsafe ref int Length => ref Unsafe.AsRef<int>((void*)(Handle + IntPtr.Size + (sizeof(int) * 2)));

        public bool IsEmpty => Length <= 0 || Data == IntPtr.Zero;

        public bool IsExternallyAllocated =>
            (GrowSize & (ExternalConstBufferMarker | ExternalBufferMarker)) != 0;

        public bool IsReadOnly => (GrowSize & ExternalConstBufferMarker) != 0;

        public unsafe Span<byte> Bytes =>
            IsEmpty ? Span<byte>.Empty : new Span<byte>(Data.ToPointer(), Length);
    }
}
