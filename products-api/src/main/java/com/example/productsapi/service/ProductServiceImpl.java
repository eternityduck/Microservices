package com.example.productsapi.service;

import com.example.productsapi.data.ProductRepository;
import com.example.productsapi.data.UserRepository;
import com.example.productsapi.domain.Product;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class ProductServiceImpl implements ProductService {

    final ProductRepository productRepository;
    final UserRepository userRepository;

    @Override
    public List<Product> findAll() {
        return productRepository.findAll();
    }

    @Override
    public Optional<Product> findById(Long id) {
        return productRepository.findById(id);
    }

    @Override
    public void deleteById(Long id) {
        productRepository.deleteById(id);
    }

    @Override
    public Product save(Product product) {
        if (userRepository.existsById(product.getOwnerId())) {
            return product;
        } else {
            throw new IllegalArgumentException("It is not possible to create a product with a user (id=%d)"
                    .formatted(product.getOwnerId()));
        }
    }
}
