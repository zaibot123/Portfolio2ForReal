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

  // verify(string login_password, string hashed_registered_password, string saltstring)
  // is called from Authenticator.login()

  public virtual bool verify(string login_password, string hashed_registered_password, string saltstring) {
    string hashed_login_password = hashSHA256(login_password, saltstring);
    if (hashed_registered_password.Equals(hashed_login_password)) return true;
    else return false;
  }

  // hashSHA256 is the "workhorse" --- the actual hashing

  public string hashSHA256(string password, string saltstring) {
    byte[] hashinput = Encoding.UTF8.GetBytes(saltstring + password); // perhaps encode only the password part?
    byte[] hashoutput = sha256.ComputeHash(hashinput); 
    return Convert.ToHexString(hashoutput);
  }

  // how much time does it take to compute a bunch of hash values?

  public void hash_measurement() {
    int count = 250000;
    byte[] hashinput = {0, 0, 0, 0, 0, 0, 0, 0};
    Console.WriteLine("Doing " + count + " hash computations");
    for (int i = 0; i < count; i++) {
      hashinput = sha256.ComputeHash(hashinput);
    }
  }

  public void pbkdf2_measurement() {
    int iterations = 1000000;
    Console.WriteLine("Doing " + iterations + " in function Pbkdf2");
    string password = "admindnc";
    byte[] salt = {0, 0, 0, 0, 0, 0, 0, 0};
    KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, iterations, hash_bytesize); 
  }

}


