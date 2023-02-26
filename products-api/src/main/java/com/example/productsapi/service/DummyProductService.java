package com.example.productsapi.service;

import com.example.productsapi.domain.Product;
import org.springframework.stereotype.Service;

import java.util.Collection;
import java.util.Optional;

@Service
public class DummyProductService implements ProductService {
    @Override
    public void create(Product product) {
    }

    @Override
    public Collection<Product> readAll() {
        return null;
    }

    @Override
    public Optional<Product> readById(long id) {
        return Optional.empty();
    }

    @Override
    public void update(Product product) {
    }

    @Override
    public void deleteById(long id) {
    }
}
