-----BEGIN PGP SIGNED MESSAGE-----
Hash: SHA256

Proposal : BIP 101
Approval : No

This proposal starts at 8mb then continues to double until 8gb.

Chinese miners[1] have indicated 8mb is their maximum tolerance
[1] yet proposal continues to increase to 8gb.

75% acceptance breaks from the established 95% threshold for
supermajority enforcement. With this scheme there is high risk of
early activation[2].

There is no cut-off date for the proposal to expire if it fails
to reach supermajority.

This proposal interfers with normal block versioning by signalling
using version bits[3] (which has not yet finalised) and thus interferes
with the standard rollout of softforking[4].

<ol>
<li><a href="http://7fvhfe.com1.z0.glb.clouddn.com/@/wp-content/uploads/2015/06/%E5%8C%BA%E5%9D%97%E6%89%A9%E5%AE%B9%E8%8D%89%E6%A1%88.jpg">Statement by Chinese miners</a></li>
<li><a href="http://organofcorti.blogspot.de/2015/08/bip101-implementation-flaws.html">BIP101 Implementation flaws</a></li>
<li><a href="https://gist.github.com/sipa/bf69659f43e763540550">Version Bits proposal</a></li>
<li><a href="http://lists.linuxfoundation.org/pipermail/bitcoin-dev/2015-August/010396.html">Deployment considerations</a></li>
</ol>
gpg: NOTE: signature key D629FA40 has been revoked
gpg: using subkey 3A31E956 instead of primary key E73A1AF2
gpg: RSA/SHA256 signature from: "3A31E956 BtcDrak <btcdrak@gmail.com>"
-----BEGIN PGP SIGNATURE-----
Version: GnuPG v2

iQEcBAEBCAAGBQJV4LSkAAoJEPxkNPw6MelWA8UH/jrnogM/AiEGjWoSLIkSOEXv
8iCMduu8DiZK7j4BzWqeIwYSIfAt1ebhvPGHeBMi6lrCTkz9d4JMo0PtCQ3Wwqlm
WTGGb6GeW/wsyNIK6/Of/SRBtpiBv3PqTViiNGDUSkOBdGoM+qh+ufWQRzAve/eL
E6Qv8yjGb3AB5EkxR9y31vrAY/xtm9gLgVTqcRxINdsm5MzPUO0Pr8y+f8dmmU9R
oP5vUvEw4StRNSPZB9qxeWu8OMLsDbnl9vM7yiz18yzKm0/T8YgkCLvypXXJFq3a
VTC1fsL9n61o2teSHNRhRLxaI4ZZinCNyZDPmR0bIrvqViGU5RutqJw9bSvqZ4c=
=njzO
-----END PGP SIGNATURE-----