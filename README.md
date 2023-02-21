# Microservices

### Створення Інгрес локально
```
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/provider/cloud/deploy.yaml
```
[Доки по інгресу](https://kubernetes.github.io/ingress-nginx/deploy/#quick-start)
### Створення неймспейсу
```
kubectl apply -f k8s/Common/app_namespace.yaml
```

### Створення сервісів
```
kubectl apply -f k8s/orders-api/
```

### Доступ до orders-api
```
localhost/orders
```
