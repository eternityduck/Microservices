package com.example.productsapi.web.dto;

import com.example.productsapi.domain.Product;
import org.mapstruct.*;

import java.util.List;

@Mapper(unmappedTargetPolicy = ReportingPolicy.IGNORE)
public interface ProductMapper {
    Product toEntity(ProductDto entryDto);

    @InheritInverseConfiguration(name = "toEntity")
    ProductDto toDto(Product entry);

    List<ProductDto> toDto(List<Product> employees);

    @InheritConfiguration(name = "toEntity")
    @BeanMapping(nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE)
    Product partialUpdate(ProductDto entryDto, @MappingTarget Product entry);
}