using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace Trinity.Encore.Framework.Game.IO
{
    public sealed class DB2Reader<T> : ClientDbReader<T>
        where T : class, IClientDbRecord, new()
    {
        /// <summary>
        /// DB2 file header magic number (WDB2).
        /// </summary>
        public const int HeaderMagicNumber = 0x32424457;

        public int RecordCount { get; private set; }

        public int FieldCount { get; private set; }

        public int RecordSize { get; private set; }

        public int TableHash { get; private set; }

        public int Build { get; private set; }

        public int Unknown1 { get; private set; }

        public int Unknown2 { get; private set; }

        public int Unknown3 { get; private set; }

        public ClientLocale Locale { get; private set; }

        public int Unknown4 { get; private set; }

        public DB2Reader(string fileName)
            : base(fileName)
        {
            Contract.Requires(!string.IsNullOrEmpty(fileName));
        }

        public override int HeaderMagic
        {
            get { return HeaderMagicNumber; }
        }

        protected override byte[] ReadData(BinaryReader reader)
        {
            RecordCount = reader.ReadInt32();
            FieldCount = reader.ReadInt32();
            RecordSize = reader.ReadInt32();
            StringTableSize = reader.ReadInt32();
            TableHash = reader.ReadInt32();
            Build = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            Unknown2 = reader.ReadInt32();
            Unknown3 = reader.ReadInt32();
            Locale = (ClientLocale)reader.ReadInt32();
            Unknown4 = reader.ReadInt32();

            // No idea what these are...
            if (Unknown3 != 0)
            {
                var size = Unknown3 * 4 - 48;
                Contract.Assume(size > 0);

                reader.ReadBytes(size);
                reader.ReadBytes(size * 2);
            }

            // Read in all the records.
            var count = RecordCount * RecordSize;
            Contract.Assume(count >= 0);
            return reader.ReadBytes(count);
        }
    }
}