package com.example.productsapi.web;

import org.springframework.data.rest.webmvc.RepositoryRestController;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@RepositoryRestController(path = "/products")
public class ProductController {

    @GetMapping("/fail")
    public @ResponseBody ResponseEntity<Object> getProducers() {
        System.exit(-1);
        return null;
    }

}
