using BIPApproval;
using BIPApproval.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class Class1
    {
        [Fact]
        public void CanCheckSig()
        {
            var pubkey = File.ReadAllBytes("../../Data/nico.asc");
            var sig = File.ReadAllText("../../Data/signature.sig");
            string txt = null;
            var result = CryptoHelper.VerifySig(pubkey, sig, out txt);
            DevViewModel dev = new DevViewModel();
            dev.Load(txt);
            Assert.True(dev.Opinions.Count == 2);
            Assert.True(result);

            pubkey = File.ReadAllBytes("../../Data/nico.pgp");
            Assert.True(CryptoHelper.VerifySig(pubkey, sig, out txt));

            Assert.True(CryptoHelper.VerifySig(CryptoHelper.ToAsc(pubkey), sig, out txt));
        }
    }
}
