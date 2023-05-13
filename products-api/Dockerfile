FROM amazoncorretto:17-alpine-full

WORKDIR /app

COPY .mvn .mvn
COPY mvnw pom.xml ./
RUN ./mvnw install

COPY src src

ENTRYPOINT ["./mvnw", "spring-boot:run"]
EXPOSE 8080