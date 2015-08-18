using BIPApproval;
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
            var sig = File.ReadAllBytes("../../Data/signature.sig");
            var result = CryptoHelper.VerifySig(pubkey, sig);
            Assert.True(result);
        }
    }
}
