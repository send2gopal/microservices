# microservices

#High level diagram

![image](https://user-images.githubusercontent.com/14090712/154395597-13817897-2c0c-4557-b8e0-4bd657af2e04.png)


// FOr exposing outside
c:/> minikube tunnel

//Get Dashboard token
kubectl -n kubernetes-dashboard get secret $(kubectl -n kubernetes-dashboard get sa/admin-user -o jsonpath="{.secrets[0].name}") -o go-template="{{.data.token | base64decode}}"

eyJhbGciOiJSUzI1NiIsImtpZCI6ImpDNUc1aEk4dXpZN18zZExSX3lKQzBHTjl6WDJGS1lhWHRTY2xhTkt6RzAifQ.eyJpc3MiOiJrdWJlcm5ldGVzL3NlcnZpY2VhY2NvdW50Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9uYW1lc3BhY2UiOiJrdWJlcm5ldGVzLWRhc2hib2FyZCIsImt1YmVybmV0ZXMuaW8vc2VydmljZWFjY291bnQvc2VjcmV0Lm5hbWUiOiJhZG1pbi11c2VyLXRva2VuLWxzODJyIiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9zZXJ2aWNlLWFjY291bnQubmFtZSI6ImFkbWluLXVzZXIiLCJrdWJlcm5ldGVzLmlvL3NlcnZpY2VhY2NvdW50L3NlcnZpY2UtYWNjb3VudC51aWQiOiJlYjAzYzNkYi03ZGIxLTQ5OGUtYmU1Ni03YmE0ZWQzYjRhNWEiLCJzdWIiOiJzeXN0ZW06c2VydmljZWFjY291bnQ6a3ViZXJuZXRlcy1kYXNoYm9hcmQ6YWRtaW4tdXNlciJ9.mUXYLSpG712RqyUlZfRxISWKJn1fip3elrar3LVE4mcuiOb9bVzpm6Z5x81MPUho-AEKPFIuIPp4tSxpLHjeDoQzZXpxEr4f7wQmPERxHfqpz9Ai1d7H2sP7gIfvvYQZSsNoq6CO00rzOZMQmDCn2c7ztzEKWr4g5pxIjLvl9LcBOiPC5QxHKkg1zprP_dco8biyEWkjr7XUBGgbjT87N59dbKOb2qY05U7ogarQMC8RgsvOKkq8RhI_6-h1UlwQOGO2jgHv22xkfVbYziwsYR9cPi9qX1X9sPG3eTZPJ9BpHeCVnDO5AkDjh58t5SDtZa3YGeaJyJlQcO8XkZzZkA


# install dapr as control plane

helm upgrade --install dapr dapr/dapr --version=1.6 --namespace microkart-app --create-namespace --wait
helm uninstall dapr --namespace dapr-system

helm upgrade --install microkart-app ./micorkart-app
helm upgrade --install microkart-app .\microkart-app\


kubectl create role access-secrets --verb=get,list,watch,update,create --resource=secrets
kubectl create rolebinding --role=access-secrets default-to-secrets --serviceaccount=microkart-app:default

dapr in Kubernetes
https://www.youtube.com/watch?v=1PXGfn0nHYE



Kubernetes + Dapr Part 1 Service to Service HTTP Calls
https://www.youtube.com/watch?v=ulPM-vL1bmY&t=266s