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
helm dep build helm &&
helm dep build helm/charts/orders-api &&
helm dep build helm/charts/products-api && 
helm dep build helm/charts/users-api &&
helm dep build helm/charts/frontend

helm install local helm
helm uninstall local
```

## Important
Delete pvcs before second redeploy
```
kubectl delete pvc/data-postgres-0
```

### Testing
```
Make the request handling slow (10 sec latency; users-api): POST http://localhost/users/untested-request
```
#### Testing results:
**Normal** pods:

![normal pods testing](/test/results/1-normal.png)

One **unhealthy** users-api pod:

![1 unhealthy users-api pod testing](/test/results/2-broken.png)

One unhealthy users-api pod + **retry**:

![1 unhealthy users-api pod testing (with retry)](/test/results/3-broken-retry.png)

One unhealthy users-api pod + **circuit breaker**:

![1 unhealthy users-api pod testing (with circuit breaker)](/test/results/4-broken-circuit-breaker.png)


### ISTIO
```
kubectl create namespace istio-system
helm repo add istio https://istio-release.storage.googleapis.com/charts
helm install istio-base istio/base -n istio-system
helm install istiod istio/istiod -n istio-system --wait
helm repo update
kubectl label namespace default istio-injection=enabled
kubectl apply -f istio 
```
