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

### Create database
```
kubectl apply -f k8s/postgres/
```

### Create services
```
kubectl apply -f k8s/orders-api/
kubectl apply -f k8s/products-api/
kubectl apply -f k8s/users-api/
```

### Create UI
```
kubectl apply -f k8s/frontend/
```

### Access to services
```
localhost/orders
localhost/products
localhost/users
```

### Access to UI
```
localhost/
```

### HELM
```
helm dep build helm
helm dep build helm/charts/<service-name>

helm install <release-name> helm
helm uninstall <release-name>
```

## Important
Delete pvcs before second redeploy
```
kubectl delete pvc/data-postgres-0
```

### ISTIO
```
kubectl create namespace istio-system
helm repo add istio https://istio-release.storage.googleapis.com/charts
helm install istiod istio/istiod -n istio-system --wait
helm repo update
kubectl label namespace default istio-injection=enabled
kubectl apply -f istio 
```