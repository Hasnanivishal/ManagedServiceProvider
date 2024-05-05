Kubectl apply -f rabbitmq-depl.yaml
Kubectl apply -f mongodb-profile-deployment.yaml

Kubectl apply -f coupons-deployment.yaml
Kubectl apply -f subscriptions-deployment.yaml
Kubectl apply -f profiles-deployment.yaml
Kubectl apply -f orders-deployment.yaml

Kubectl get deployments
Kubectl get pods
Kubectl get services
Kubectl get StatefulSet