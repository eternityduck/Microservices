package com.example.productsapi.web.dto;

import com.example.productsapi.domain.Product;
import org.mapstruct.Mapper;
import org.mapstruct.MappingTarget;

import static org.mapstruct.MappingConstants.ComponentModel.SPRING;

@Mapper(componentModel = SPRING)
public interface ProductMapper {
    GetProductPayload toGetPayload(Product product);

    void updateProductFromPayload(UpdateProductPayload payload, @MappingTarget Product product);
}
