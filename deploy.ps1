# Install k8s dashboard
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v1.10.1/src/deploy/recommended/kubernetes-dashboard.yaml

kubectl apply -f .\k8s\dashboard-adminuser.yaml
kubectl -n kube-system describe secret admin-user -n kubernetes-dashboard
Write-Host "Use user secret to login dashboard" -ForegroundColor Red

# Update HELM repo
helm repo update

# Install dapt to cluster
Write-Host "Installing dapr to cluster" -ForegroundColor Green
#helm upgrade --install dapr dapr/dapr --version=1.7 --namespace dapr-system --create-namespace --wait
Write-Host "Dapr installation finished" -ForegroundColor Green

# Install seq server to cluster
Write-Host "Installing SEQ Loging server to cluster" -ForegroundColor Green
#helm install  seq-server datalust/seq --wait
Write-Host "SEQ server installation finished" -ForegroundColor Green

# Install rabbitMQ to cluster
Write-Host "Installing rabbitMQ server to cluster" -ForegroundColor Green
kubectl create namespace microkart-messages
helm install rabbitmq bitnami/rabbitmq  --set auth.password=Tqbnu4OICX  -n microkart-messages --wait
Write-Host "rabbitMQ server installation finished" -ForegroundColor Green

# Install the app to cluster
Write-Host "Installing rabbitMQ server to cluster" -ForegroundColor Green
kubectl create namespace  microkart-app-database
helm install  database-server .\k8s\database\ --wait
helm install  microkart-app .\k8s\microkart-app\ --wait
