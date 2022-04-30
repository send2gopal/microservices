# microservices

#High level diagram

![image](https://user-images.githubusercontent.com/14090712/154395597-13817897-2c0c-4557-b8e0-4bd657af2e04.png)


// FOr exposing outside
c:/> minikube tunnel

//Get Dashboard token
Refrence: https://github.com/kubernetes/dashboard
Install Dashboard
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.5.0/aio/deploy/recommended.yaml

kubectl apply -f .\createuser.yaml

kubectl -n kubernetes-dashboard get secret $(kubectl -n kubernetes-dashboard get sa/admin-user -o jsonpath="{.secrets[0].name}") -o go-template="{{.data.token | base64decode}}"

eyJhbGciOiJSUzI1NiIsImtpZCI6IjNfYzIxY3VtMmtXOXo4UjBydWxjSXJiZzdkVjhTUXAtbHRtLXNka2JUdzAifQ.eyJpc3MiOiJrdWJlcm5ldGVzL3NlcnZpY2VhY2NvdW50Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9uYW1lc3BhY2UiOiJrdWJlcm5ldGVzLWRhc2hib2FyZCIsImt1YmVybmV0ZXMuaW8vc2VydmljZWFjY291bnQvc2VjcmV0Lm5hbWUiOiJhZG1pbi11c2VyLXRva2VuLXRyNjR0Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9zZXJ2aWNlLWFjY291bnQubmFtZSI6ImFkbWluLXVzZXIiLCJrdWJlcm5ldGVzLmlvL3NlcnZpY2VhY2NvdW50L3NlcnZpY2UtYWNjb3VudC51aWQiOiJhZjQ5MDg3My1hMjFlLTRmZTYtYTNhNi01NTA2YzFhOWNlMzAiLCJzdWIiOiJzeXN0ZW06c2VydmljZWFjY291bnQ6a3ViZXJuZXRlcy1kYXNoYm9hcmQ6YWRtaW4tdXNlciJ9.Nng3XSWTe6yvr6V9B0_xGUMaXcnONz8-N4hJxac2fMHEV_PRXTnWKfxJEdUWHX6NkvceBQ5RfLwte8SnmyKNvYGWN0t7SUvf9f2gX2WJcoQsbVr_KsBYPMVG951_M66FqG-qzoXYrIj-aVhSuY_Mme8neGkg92O0JzvG7CtjgnSoUndgqWYWDyeo7OLKWfXx_RyxCNc9-tG80un8HB47b9xryWB4srsyFVs_Wcdc2Kd8sQ4jTh3DlaFO1u2Lg80cW9FzrsmC3i7wjgRoDNxu-OnISMC8NbHf8ZGy0Jo9S36xjc-MuVUY6_OMyyj1C92g_rye6Le4yVGBsH4_-9xHWg

# install dapr as control plane

helm upgrade --install dapr dapr/dapr --version=1.6 --namespace dapr-system --create-namespace --wait
helm upgrade --install dapr dapr/dapr --version=1.7 --namespace dapr-system --create-namespace --wait
helm uninstall dapr --namespace dapr-system

helm upgrade --install microkart-app ./micorkart-app
helm upgrade --install microkart-app .\microkart-app\

Change namespace to microkart-app
kubectl create role access-secrets --verb=get,list,watch,update,create --resource=secrets
kubectl create rolebinding --role=access-secrets default-to-secrets --serviceaccount=microkart-app:default

dapr in Kubernetes
https://www.youtube.com/watch?v=1PXGfn0nHYE



Kubernetes + Dapr Part 1 Service to Service HTTP Calls
https://www.youtube.com/watch?v=ulPM-vL1bmY&t=266s

# Add Logging
https://docs.dapr.io/operations/monitoring/logging/fluentd/

# Expose kibana to 5601
kubectl port-forward svc/kibana-kibana 5601 -n dapr-monitoring
# Start Proxy 
Kubectl proxy

# Expose seq to ndeport
-
kubectl port-forward svc/seq-server 5602:80 -n default

# rabbit MQ
User: user
Password: Tqbnu4OICX
ErLang Cookie : ml5z0mogmWBOp7g8b7s4UFxR1VtBGgl2

To Access the RabbitMQ AMQP port:

    echo "URL : amqp://127.0.0.1:5672/"
    kubectl port-forward --namespace microkart-messages svc/rabbitmq-server 5672:5672

To Access the RabbitMQ Management interface:

    echo "URL : http://127.0.0.1:15672/"
    kubectl port-forward --namespace microkart-messages svc/rabbitmq-server 15672:15672
