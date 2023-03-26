import { MigrationInterface, QueryRunner } from "typeorm";

export class ordersTable1679940251693 implements MigrationInterface {
    name = 'ordersTable1679940251693'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`CREATE TABLE "orders" ("id" SERIAL NOT NULL, "userId" integer NOT NULL, "orderDate" TIMESTAMP NOT NULL, "shipDate" TIMESTAMP NOT NULL, "shipAddress" character varying NOT NULL, "status" character varying NOT NULL, CONSTRAINT "PK_710e2d4957aa5878dfe94e4ac2f" PRIMARY KEY ("id"))`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`DROP TABLE "orders"`);
    }

}
