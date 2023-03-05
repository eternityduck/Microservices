package com.example.productsapi.service;

import com.example.productsapi.domain.Product;
import org.springframework.stereotype.Service;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Optional;

@Service
public final class DummyProductService implements ProductService {

    private final Map<Long, Product> products = new HashMap<>(Map.of(
            1L, new Product(1L, "Test-1", "Test-11", 2D, 3L),
            2L, new Product(2L, "Test-2", "Test-22", 3D, 4L)
    ));

    @Override
    public void create(Product product) {
        products.put(product.getId(), product);
    }

    @Override
    public Collection<Product> readAll() {
        return products.values();
    }

    @Override
    public Optional<Product> readById(long id) {
        return Optional.ofNullable(products.get(id));
    }

    @Override
    public void update(Product product) {
        products.put(product.getId(), product);
    }

    @Override
    public void deleteById(long id) {
        products.remove(id);
    }
}
