package com.example.productsapi.data;

import com.example.productsapi.domain.User;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.AccessLevel;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Repository;
import org.springframework.web.client.RestTemplate;

import java.net.URI;

@Repository
@FieldDefaults(level = AccessLevel.PRIVATE)
@Slf4j
public class UserRepositoryImpl implements UserRepository {

    final RestTemplate restTemplate = new RestTemplate();
    final ObjectMapper objectMapper = new ObjectMapper();

    @Value("${user-service.uri}")
    String uri;

    @Override
    public boolean existsById(int id) {
        try {
            URI uriId = URI.create(String.join("/", uri, String.valueOf(id)));
            log.info(uriId.toString());
            objectMapper.readValue(restTemplate.getForEntity(uriId, String.class).getBody(), User.class);
        } catch (JsonProcessingException e) {
            return false;
        }
        return true;
    }
}
