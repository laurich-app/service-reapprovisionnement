using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using Consul;
using Reapprovisionnement.Models;

namespace Securities
{

// Classe singleton
public class JWTDecoder
{
    private string consulHost = Environment.GetEnvironmentVariable("CONSUL_HOST") ?? "localhost";
    private string consulPort = Environment.GetEnvironmentVariable("CONSUL_PORT") ?? "8500";

    private static JWTDecoder instance = null;
    private JWTDecoder() { 
        // Initialisation du singleton
        this.securityKey = GetCertificateAsync().Result;
    }
    private static object lockThis = new object();

    public static JWTDecoder GetInstance
    {
        get
        {
            lock (lockThis)
            {
                if (instance == null)
                instance = new JWTDecoder();

                return instance;
            }
        }
    }
		private SecurityKey securityKey;

			private async Task<SecurityKey> GetCertificateAsync()
        {
            // URL du service qui fournit le certificat contenant la clé publique RSA
            string certServiceUrl = "http://"+this.consulHost+":"+this.consulPort+"/v1/kv/config/application/publicKey";

            using (HttpClient client = new HttpClient())
            {
                // Effectuer une requête GET vers le service pour récupérer le certificat
                try {
                    HttpResponseMessage response = await client.GetAsync(certServiceUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Lire le contenu de la réponse (le certificat)
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<KeyValueItem> keyValueItems = JsonConvert.DeserializeObject<List<KeyValueItem>>(jsonContent);

                        // Extraire les données du premier élément de keyValueItems
                        KeyValueItem firstItem = keyValueItems.FirstOrDefault();
                        if (firstItem != null)
                        {
                            // Décodage Base64
                            byte[] decodedKeyBytes = Convert.FromBase64String(firstItem.Value);

                            // Convertir les bytes en une chaîne de caractères
                            string decodedKeyString = Encoding.Default.GetString(decodedKeyBytes);

                            // Supprimer les en-têtes PEM
                            string publicKeyPEM = decodedKeyString
                                .Replace("-----BEGIN PUBLIC KEY-----", "")
                                .Replace("-----END PUBLIC KEY-----", "")
                                .Replace("\n", "");

                            // Convertir la chaîne de caractères en un tableau de bytes (byte[])
                            byte[] encoded = Convert.FromBase64String(publicKeyPEM);

                            var rsa = RSA.Create();
                            rsa.ImportSubjectPublicKeyInfo(encoded, out _);
                            Console.WriteLine("Clé récupéré");
                            return new RsaSecurityKey(rsa);
                        }
                        else
                        {
                            // Gérer le cas où keyValueItems est vide
                            throw new InvalidOperationException("No items found in the response.");
                        }
                    }
                    else
                    {
                        throw new HttpRequestException($"Failed to retrieve certificate. Status code: {response.StatusCode}");
                    }
                } catch (Exception e ){
                    Console.WriteLine(e);
                    throw new HttpRequestException("Erreur");
                }
                
                
            }
        }

    public SecurityKey GetSecurityKey()
    {
        // Méthode du singleton
				return this.securityKey;
    }
}
}

