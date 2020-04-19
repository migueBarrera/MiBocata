using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace MiBocataAPI.Helpers
{
	public static class Hash
	{
		private const string Salt = "·$&%$/%&(?¿ç¨Ç^*7z/*t*kNretrtrty7647a-A]*=Cg.42}5^Qxx&xq@-!(,gdfg-.54756";

		public static string Create(string value)
		{
			var valueBytes = KeyDerivation.Pbkdf2(
								password: value,
								salt: Encoding.UTF8.GetBytes(Salt),
								prf: KeyDerivationPrf.HMACSHA512,
								iterationCount: 10000,
								numBytesRequested: 256 / 8);

			return Convert.ToBase64String(valueBytes);
		}

		public static bool Validate(string value, string hash)
			=> Create(value) == hash;
	}
}