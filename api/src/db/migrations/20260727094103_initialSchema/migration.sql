CREATE TYPE "blood_type" AS ENUM('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-');--> statement-breakpoint
CREATE TYPE "eye_color" AS ENUM('brown', 'blue', 'green', 'hazel', 'gray', 'amber');--> statement-breakpoint
CREATE TYPE "license_status" AS ENUM('active', 'revoked', 'expired');--> statement-breakpoint
CREATE TYPE "license_type" AS ENUM('professional', 'nonProfessional', 'studentPermit');--> statement-breakpoint
CREATE TYPE "sex" AS ENUM('male', 'female');--> statement-breakpoint
CREATE TYPE "ticket_status" AS ENUM('unsettled', 'settled', 'dismissed');--> statement-breakpoint
CREATE TYPE "user_role" AS ENUM('civilian', 'officer', 'supervisor', 'admin');--> statement-breakpoint
CREATE TABLE "licenses" (
	"id" integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY (sequence name "licenses_id_seq" INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START WITH 1 CACHE 1),
	"number" text NOT NULL UNIQUE,
	"type" "license_type" NOT NULL,
	"status" "license_status" NOT NULL,
	"issuance_date" date NOT NULL,
	"expiry_date" date NOT NULL,
	"first_name" text NOT NULL,
	"middle_name" text,
	"last_name" text NOT NULL,
	"sex" "sex" NOT NULL,
	"date_of_birth" date NOT NULL,
	"address" text NOT NULL,
	"eye_color" "eye_color" NOT NULL,
	"height" integer NOT NULL,
	"weight" integer NOT NULL,
	"blood_type" "blood_type" NOT NULL
);
--> statement-breakpoint
CREATE TABLE "ticket_violations" (
	"ticket_id" integer,
	"violation_id" integer,
	"fine" numeric NOT NULL,
	CONSTRAINT "ticket_violations_pkey" PRIMARY KEY("ticket_id","violation_id")
);
--> statement-breakpoint
CREATE TABLE "tickets" (
	"id" integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY (sequence name "tickets_id_seq" INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START WITH 1 CACHE 1),
	"license_id" integer NOT NULL,
	"reference_number" text NOT NULL UNIQUE,
	"created_at" timestamp with time zone DEFAULT now() NOT NULL,
	"status" "ticket_status" NOT NULL,
	"date_of_incident" date NOT NULL,
	"place_of_incident" text NOT NULL,
	"officer_notes" text
);
--> statement-breakpoint
CREATE TABLE "users" (
	"id" integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY (sequence name "users_id_seq" INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START WITH 1 CACHE 1),
	"email" text NOT NULL UNIQUE,
	"password" text NOT NULL,
	"role" "user_role" NOT NULL,
	"license_id" integer UNIQUE
);
--> statement-breakpoint
CREATE TABLE "vehicles" (
	"id" integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY (sequence name "vehicles_id_seq" INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START WITH 1 CACHE 1),
	"plate_number" text NOT NULL UNIQUE,
	"mv_file_number" text UNIQUE,
	"vin" text UNIQUE,
	"issuance_date" date NOT NULL,
	"expiry_date" date NOT NULL,
	"make" text NOT NULL,
	"model" text NOT NULL,
	"year" integer NOT NULL,
	"color" text NOT NULL,
	"license_id" integer
);
--> statement-breakpoint
CREATE TABLE "violations" (
	"id" integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY (sequence name "violations_id_seq" INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START WITH 1 CACHE 1),
	"name" text NOT NULL UNIQUE,
	"is_tiered" boolean NOT NULL,
	"initial_fine" numeric NOT NULL,
	"second_fine" numeric,
	"third_fine" numeric
);
--> statement-breakpoint
ALTER TABLE "ticket_violations" ADD CONSTRAINT "ticket_violations_ticket_id_tickets_id_fkey" FOREIGN KEY ("ticket_id") REFERENCES "tickets"("id") ON DELETE RESTRICT;--> statement-breakpoint
ALTER TABLE "ticket_violations" ADD CONSTRAINT "ticket_violations_violation_id_violations_id_fkey" FOREIGN KEY ("violation_id") REFERENCES "violations"("id") ON DELETE RESTRICT;--> statement-breakpoint
ALTER TABLE "tickets" ADD CONSTRAINT "tickets_license_id_licenses_id_fkey" FOREIGN KEY ("license_id") REFERENCES "licenses"("id") ON DELETE RESTRICT;--> statement-breakpoint
ALTER TABLE "users" ADD CONSTRAINT "users_license_id_licenses_id_fkey" FOREIGN KEY ("license_id") REFERENCES "licenses"("id") ON DELETE SET NULL;--> statement-breakpoint
ALTER TABLE "vehicles" ADD CONSTRAINT "vehicles_license_id_licenses_id_fkey" FOREIGN KEY ("license_id") REFERENCES "licenses"("id") ON DELETE SET NULL;