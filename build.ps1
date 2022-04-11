 docker build -f microkart.catalog/Dockerfile -t microkart-catalog:v1 .

 docker build -f microkart.identity/Dockerfile -t microkart-identity:v1 .

 docker build -f microkart.frontend/Dockerfile -t microkart-frontend:v1 . --progress=plain --no-cache