# Microservices

### Create ingress locally
```
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/provider/cloud/deploy.yaml
```
[Ingress Docs](https://kubernetes.github.io/ingress-nginx/deploy/#quick-start)
### Create namespace
```
kubectl apply -f k8s/Common/app_namespace.yaml
```

### Create services
```
kubectl apply -f k8s/orders-api/
kubectl apply -f k8s/products-api/
kubectl apply -f k8s/users-api/
```

### Access to services
```
localhost/orders
localhost/products
localhost/users
```
