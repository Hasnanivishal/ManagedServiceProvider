# ManagedServiceProvider

## 4 Serviece - Profile, Order, Coupn & Subscription

Flow Chart

![project-flow-chart](https://i.sstatic.net/OSqavr18.png)

# Commands

* Docker Commands - Build the Image and publish to docker hub

```YAML
docker build -t hasnanivishal/msp.profile .
docker build -t hasnanivishal/msp.order .
docker build -t hasnanivishal/msp.coupn .
docker build -t hasnanivishal/msp.subscription .

docker push hasnanivishal/msp.profile
docker push hasnanivishal/msp.order
docker push hasnanivishal/msp.coupn
docker push hasnanivishal/msp.subscription
```

* Kubernetes Commands - Apply the YAML files

```YAML

Kubectl apply -f rabbitmq-depl.yaml
Kubectl apply -f mongodb-profile-deployment.yaml

Kubectl apply -f coupons-deployment.yaml
Kubectl apply -f subscriptions-deployment.yaml
Kubectl apply -f profiles-deployment.yaml
Kubectl apply -f orders-deployment.yaml
```

* K8S General Commands ->

```YAML

Kubectl get deployments
Kubectl get pods
Kubectl get services
Kubectl get StatefulSet

kubectl rollout restart deployment <deployment-name>

kubectl delete deployment <name>
kubectl delete service <name>
kubectl delete pod <name>

```