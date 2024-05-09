Kubectl apply -f ManagedServiceProvider/Kubernetes-Deployment/rabbitmq-depl.yaml
Kubectl apply -f ManagedServiceProvider/Kubernetes-Deployment/mongodb-profile-deployment.yaml
kubectl apply -f ManagedServiceProvider/Kubernetes-Deployment/redis-profile-deployment.yaml

Kubectl apply -f ManagedServiceProvider/Kubernetes-Deployment/coupons-deployment.yaml
Kubectl apply -f ManagedServiceProvider/Kubernetes-Deployment/subscriptions-deployment.yaml
Kubectl apply -f ManagedServiceProvider/Kubernetes-Deployment/profiles-deployment.yaml
Kubectl apply -f ManagedServiceProvider/Kubernetes-Deployment/orders-deployment.yaml

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.1/deploy/static/provider/cloud/deploy.yaml
Kubectl apply -f ManagedServiceProvider/Kubernetes-Deployment/ingress-srv-deployment.yaml

Kubectl get deployments
Kubectl get pods
Kubectl get services
Kubectl get StatefulSet

Kubectl get Ingress

kubectl get services --namespace=ingress-nginx

