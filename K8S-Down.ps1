kubectl delete deployment coupon-depl
kubectl delete deployment order-depl
kubectl delete deployment profile-depl
kubectl delete deployment subscription-depl
kubectl delete deployment rabbitmq-depl

kubectl delete service coupon-clusterip-srv
kubectl delete service mongodb-profile-clusterip-srv
kubectl delete service order-clusterip-srv
kubectl delete service orderservice-srv
kubectl delete service profile-clusterip-srv
kubectl delete service profileservice-srv
kubectl delete service rabbitmq-clusterip-srv
kubectl delete service rabbitmq-loadbalancer
kubectl delete service subscription-clusterip-srv

kubectl delete pod mongodb-0
kubectl delete pod redis-0
kubectl delete StatefulSet mongodb
kubectl delete pvc mongodb-data-mongodb-0

kubectl delete --all service
kubectl delete --all deployment
kubectl delete --all pod

