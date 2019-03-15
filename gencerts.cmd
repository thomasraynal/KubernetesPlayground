set services= "test-app" "test-api"

for %%s in (%services%) do (

openssl genrsa -des3 -passout pass:idkfa -out %%s.key 2048
openssl rsa -passin pass:idkfa -in %%s.key -out %%s.key
openssl req -sha256 -new -subj /CN=%%s -key %%s.key -out %%s.csr 
openssl x509 -req -sha256 -days 365 -in %%s.csr -signkey %%s.key -out %%s.crt -extfile %%s.conf
openssl pkcs12 -export -out %%s.pfx -inkey %%s.key -in %%s.crt -passout pass:idkfa

del %%s.csr

kubectl delete secret %%s
kubectl create secret tls %%s-secret --cert %%s.crt --key %%s.key

)
