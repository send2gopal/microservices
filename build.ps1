 docker build -f microkart.catalog/Dockerfile -t microkart-catalog:latest .

 docker build -f microkart.identity/Dockerfile -t microkart-identity:latest .

 docker build -f microkart.basket/Dockerfile -t microkart-basket:latest .
 
 docker build -f microkart.order/Dockerfile -t microkart-order:latest .

 docker build -f microkart.payment/Dockerfile -t microkart-payment:latest .

 docker build -f microkart.notification/Dockerfile -t microkart-notification:latest .

 docker build  -t microkart-frontend:latest -f microkart.frontend/Dockerfile . --progress=plain --no-cache

 helm uninstall  microkart-app --wait

 helm install  microkart-app .\k8s\microkart-app\ --wait