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

eyJhbGciOiJSUzI1NiIsImtpZCI6IlBKTjhlY1ZzbFJ1S3pZVk82VndCcjRueWZGT085Y3g5SDB1emdMWUdZcWMifQ.eyJpc3MiOiJrdWJlcm5ldGVzL3NlcnZpY2VhY2NvdW50Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9uYW1lc3BhY2UiOiJrdWJlcm5ldGVzLWRhc2hib2FyZCIsImt1YmVybmV0ZXMuaW8vc2VydmljZWFjY291bnQvc2VjcmV0Lm5hbWUiOiJhZG1pbi11c2VyLXRva2VuLTdqbWo2Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9zZXJ2aWNlLWFjY291bnQubmFtZSI6ImFkbWluLXVzZXIiLCJrdWJlcm5ldGVzLmlvL3NlcnZpY2VhY2NvdW50L3NlcnZpY2UtYWNjb3VudC51aWQiOiI2MjUyOWYyNy1iMGY3LTQwZGItOTgyOS0yNGQ1YTcxOWZiOTQiLCJzdWIiOiJzeXN0ZW06c2VydmljZWFjY291bnQ6a3ViZXJuZXRlcy1kYXNoYm9hcmQ6YWRtaW4tdXNlciJ9.WNtsO31_D0gZ0OQLpYBzGndj-kvDWP6w5--rhmWTziEpczd6U1ryxnLAkHhPDt-gtWQc8zDCv5c2zpRZ4IuQx837UNHyBCfkhBGLMq7wiUDnjCjAFye2cK9Y-fZa2tqLqDpe2bEh0Z91TiFOfW-aq4jV5xtLm6qCIak1Jsw7ta3MsFquBCjN9ao_q4PCIZW2kfvvrw7g6A6YOChf3GDeqAeEqEpy95Q9a92N5ShLIHFHS7RqjCNbZVz7mAfwTVd9Xqi0g3fSUg1wgXE8o4uCC2oso5McUSrkYS_m2b2AJMQMb8ac1W1kDO79s0GHxGnPmdoNva2qV2tb3DeDaBYIuA

# install dapr as control plane

helm upgrade --install dapr dapr/dapr --version=1.6 --namespace dapr-system --create-namespace --wait
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
