using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIPApproval
{
    public class CryptoHelper
    {
        public static bool VerifySig(byte[] asc, string sig, out string message)
        {
            try
            {
                foreach(PgpPublicKey pubkey in new PgpPublicKeyRing(GetStream(asc)).GetPublicKeys().OfType<PgpPublicKey>()) //java madness
                {


                    //AGAIN MADNESS THIS MAKE PERFECT SENSE !
                    ArmoredInputStream sigInput = new ArmoredInputStream(new MemoryStream(Encoding.UTF8.GetBytes(sig)));

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
                    message = Encoding.UTF8.GetString(toSign);

                    PgpObjectFactory pgpObjFactory = new PgpObjectFactory(sigInput);
                    var list = (PgpSignatureList)pgpObjFactory.NextPgpObject();
                    PgpSignature pgpSig = list[0];
                    pgpSig.InitVerify(pubkey);
                    pgpSig.Update(toSign);
                    var result = pgpSig.Verify();
                    if(result)
                        return result;
                    Regex endofline = new Regex("[ ]+?(\r?)\n");
                    message = endofline.Replace(message, "$1\n");
                    toSign = Encoding.UTF8.GetBytes(message);
                    pgpSig.InitVerify(pubkey);
                    pgpSig.Update(toSign);
                    result = pgpSig.Verify();
                    if(result)
                        return result;
                }
            }
            catch //Don't do it at home kids
            {
             
            }
            message = null;
            return false;
        }


        private static Stream GetStream(byte[] asc)
        {
            return PgpUtilities.GetDecoderStream(new MemoryStream(asc));
        }

        public static byte[] ToAsc(byte[] pgp)
        {
            MemoryStream ms = new MemoryStream();
            var ring = new PgpPublicKeyRing(GetStream(pgp));
            var armored = new ArmoredOutputStream(ms);
            ring.Encode(armored);
            armored.Dispose();
            return ms.ToArray();
        }
    }
}
