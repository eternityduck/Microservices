package com.example.productsapi.web;

import com.example.productsapi.web.dto.ExceptionDetails;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.context.request.WebRequest;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

@ControllerAdvice
public class RestResponseEntityExceptionHandler
        extends ResponseEntityExceptionHandler {

    @ExceptionHandler(IllegalArgumentException.class)
    public ResponseEntity<ExceptionDetails> handleBadRequest(RuntimeException exception, WebRequest request) {
        return handleGeneric(exception, request, HttpStatus.BAD_REQUEST);
    }

    private ResponseEntity<ExceptionDetails> handleGeneric(RuntimeException exception,
                                                           WebRequest request,
                                                           HttpStatus status) {
        return new ResponseEntity<>(
                new ExceptionDetails(
                        exception.getMessage(),
                        request.getDescription(false),
                        LocalDateTime.now().format(DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm:ss"))),
                status);
    }


}
