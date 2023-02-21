import { ClassSerializerInterceptor } from '@nestjs/common'
import { ValidationPipe } from '@nestjs/common/pipes'
import { NestFactory, Reflector } from '@nestjs/core';
import { AppModule } from './app.module';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  
  app.useGlobalPipes(new ValidationPipe({
    transform: true,
    transformOptions: {
      enableImplicitConversion: true,
    },
    forbidUnknownValues: false
  }));
  app.useGlobalInterceptors(new ClassSerializerInterceptor(app.get(Reflector)));

  await app.listen(80);
}
bootstrap();
