FROM amazoncorretto:17-alpine-full

WORKDIR /app

COPY .mvn .mvn
COPY mvnw pom.xml ./

RUN chmod +x mvnw
RUN ./mvnw dependency:go-offline

COPY src src

CMD ["./mvnw", "spring-boot:run"]
EXPOSE 8080
