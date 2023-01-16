// Hashing.cs

using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

public class Hashing {
  protected const int salt_bitsize = 64;
  protected const byte salt_bytesize = salt_bitsize/8;
  protected const int hash_bitsize = 256;
  protected const int hash_bytesize = hash_bitsize/8;
 
  private HashAlgorithm sha256 = SHA256.Create();
  protected RandomNumberGenerator rand = RandomNumberGenerator.Create();

  // hash(string password)
  // called from Authenticator.register()
  // where salt and hashed password have not been generated,
  // so both are returned for storing in the password database

  public virtual Tuple<string, string> hash(string password) {
    byte[] salt = new byte[salt_bytesize];
    rand.GetBytes(salt);
    string saltstring = Convert.ToHexString(salt);
    string hash = hashSHA256(password, saltstring);
    return Tuple.Create(hash, saltstring);
  }


  // hashSHA256 is the "workhorse" --- the actual hashing

  public string hashSHA256(string password, string saltstring) {
    byte[] hashinput = Encoding.UTF8.GetBytes(saltstring + password); // perhaps encode only the password part?
    byte[] hashoutput = sha256.ComputeHash(hashinput); 
    return Convert.ToHexString(hashoutput);

  }
}


