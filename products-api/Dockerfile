FROM amazoncorretto:17-alpine-full

WORKDIR /app

COPY .mvn .mvn
COPY mvnw pom.xml ./
COPY src src

RUN chmod +x mvnw
RUN ./mvnw install

ENTRYPOINT ["./mvnw", "spring-boot:run"]
EXPOSE 8080