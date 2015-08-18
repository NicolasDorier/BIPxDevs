using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPApproval
{
    public class CryptoHelper
    {
        public static bool VerifySig(byte[] asc, byte[] sig)
        {
            PgpPublicKey pubkey = new PgpPublicKeyRing(PgpUtilities.GetDecoderStream(new MemoryStream(asc))).GetPublicKey(); //java madness


            //AGAIN MADNESS THIS MAKE PERFECT SENSE !
            ArmoredInputStream sigInput = new ArmoredInputStream(new MemoryStream(sig));

            //
            // read the input, making sure we ingore the last newline.
            //
            int ch;
            string newLine = null;
            MemoryStream bOut = new MemoryStream();

            while((ch = sigInput.ReadByte()) >= 0 && sigInput.IsClearText())
            {
                if(newLine != null)
                {
                    foreach(var c in newLine)
                        bOut.WriteByte((byte)c);
                    newLine = null;
                }
                if(ch == '\r')
                {
                    ch = sigInput.ReadByte();
                    if(ch == '\n')
                    {
                        newLine = "\r\n";
                        continue;
                    }
                }
                if(ch == '\n')
                {
                    newLine = "\n";
                    continue;
                }

                bOut.WriteByte((byte)ch);
            }

            var toSign = bOut.ToArray();
            PgpObjectFactory pgpObjFactory = new PgpObjectFactory(sigInput);
            var list = (PgpSignatureList)pgpObjFactory.NextPgpObject();
            PgpSignature pgpSig = list[0];
            pgpSig.InitVerify(pubkey);
            pgpSig.Update(toSign);
            return pgpSig.Verify();
        }
    }
}
