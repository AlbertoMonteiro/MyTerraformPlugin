//using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
//using Org.BouncyCastle.Asn1;
//using Org.BouncyCastle.Asn1.Pkcs;
//using Org.BouncyCastle.Asn1.X509;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Generators;
//using Org.BouncyCastle.Crypto.Operators;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.Crypto.Prng;
//using Org.BouncyCastle.Math;
//using Org.BouncyCastle.OpenSsl;
//using Org.BouncyCastle.Pkcs;
//using Org.BouncyCastle.Security;
//using Org.BouncyCastle.Utilities;
//using Org.BouncyCastle.X509;

namespace MyTerraformPlugin;

public static class CertificateGenerator
{
    private const int KeyStrength = 2048;

    //public static AsymmetricKeyParameter GeneratePrivateKey()
    //{
    //    var randomGenerator = new CryptoApiRandomGenerator();
    //    var random = new SecureRandom(randomGenerator);

    //    // Generate Key
    //    var keyGenerationParameters = new KeyGenerationParameters(random, KeyStrength);
    //    var keyPairGenerator = new RsaKeyPairGenerator();
    //    keyPairGenerator.Init(keyGenerationParameters);
    //    return keyPairGenerator.GenerateKeyPair().Private;
    //}

    public static X509Certificate2 GenerateSelfSignedCertificate(string subjectName, string issuerName)
    {
        //var issuerPrivKey = GeneratePrivateKey();
        //var randomGenerator = new CryptoApiRandomGenerator();
        //var random = new SecureRandom(randomGenerator);
        //var certificateGenerator = new X509V3CertificateGenerator();

        //// Serial Number
        //var serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(long.MaxValue), random);
        //certificateGenerator.SetSerialNumber(serialNumber);

        //// Issuer and SN
        //var subjectDN = new X509Name(subjectName);
        //var issuerDN = new X509Name(issuerName);
        //certificateGenerator.SetIssuerDN(issuerDN);
        //certificateGenerator.SetSubjectDN(subjectDN);

        //// SAN
        //var subjectAltName = new GeneralNames(new GeneralName(GeneralName.DnsName, "localhost"));
        //certificateGenerator.AddExtension(X509Extensions.SubjectAlternativeName, false, subjectAltName);

        //// Validity
        //var notBefore = DateTime.UtcNow.Date;
        //var notAfter = notBefore.AddYears(2);
        //certificateGenerator.SetNotBefore(notBefore);
        //certificateGenerator.SetNotAfter(notAfter);

        //// Public Key
        //var keyGenerationParameters = new KeyGenerationParameters(random, KeyStrength);
        //var keyPairGenerator = new RsaKeyPairGenerator();
        //keyPairGenerator.Init(keyGenerationParameters);
        //var subjectKeyPair = keyPairGenerator.GenerateKeyPair();
        //certificateGenerator.SetPublicKey(subjectKeyPair.Public);

        //// Sign certificate
        //var signatureFactory = new Asn1SignatureFactory("SHA256WithRSA", issuerPrivKey, random);
        //var certificate = certificateGenerator.Generate(signatureFactory);

        //var x509 = X509CertificateLoader.LoadCertificate(certificate.GetEncoded());

        //// Private key
        //var privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(subjectKeyPair.Private);

        //var seq = (Asn1Sequence)Asn1Object.FromByteArray(privateKeyInfo.ParsePrivateKey().GetDerEncoded());
        //if (seq.Count != 9)
        //{
        //    throw new PemException("Invalid RSA private key");
        //}

        //var rsa = RsaPrivateKeyStructure.GetInstance(seq);
        //var rsaparams = new RsaPrivateCrtKeyParameters(rsa.Modulus, rsa.PublicExponent, rsa.PrivateExponent, rsa.Prime1, rsa.Prime2, rsa.Exponent1, rsa.Exponent2, rsa.Coefficient);

        //var parms = DotNetUtilities.ToRSAParameters(rsaparams);
        //var rsa1 = RSA.Create();
        //rsa1.ImportParameters(parms);

        //// https://github.com/dotnet/runtime/issues/23749
        //using var cert = x509.CopyWithPrivateKey(rsa1);

        //var certB64 = Convert.ToBase64String(cert.Export(X509ContentType.Pkcs12));
        var certB64 = """
            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
            """;

        var cert = Convert.FromBase64String(certB64);

        return X509CertificateLoader.LoadPkcs12(cert, null);
        //return X509CertificateLoader.LoadPkcs12(cert.Export(X509ContentType.Pkcs12), null);
    }
}
