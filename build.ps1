 #docker build -f microkart.catalog/Dockerfile -t microkart-catalog:v1 .

 #docker build -f microkart.identity/Dockerfile -t microkart-identity:v1 .

 #docker build -f microkart.basket/Dockerfile -t microkart-basket:v1 .

 docker build  -t microkart-frontend:v1 -f microkart.frontend/Dockerfile . --progress=plain --no-cache