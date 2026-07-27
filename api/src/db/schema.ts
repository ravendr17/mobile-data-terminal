import { boolean, date, integer, numeric, pgEnum, primaryKey, snakeCase, text, timestamp, unique } from "drizzle-orm/pg-core";
import { userRoles } from "../constants/user-roles.js";
import { licenseTypes } from "../constants/license-types.js";
import { licenseStatuses } from "../constants/license-statuses.js";
import { sexes } from "../constants/sexes.js";
import { eyeColors } from "../constants/eye-colors.js";
import { bloodTypes } from "../constants/blood-types.js";
import { ticketStatuses } from "../constants/ticket-statuses.js";

const userRoleEnum = pgEnum("user_role", userRoles);
const licenseTypeEnum = pgEnum("license_type", licenseTypes);
const licenseStatusEnum = pgEnum("license_status", licenseStatuses);
const sexEnum = pgEnum("sex", sexes);
const eyeColorEnum = pgEnum("eye_color", eyeColors);
const bloodTypeEnum = pgEnum("blood_type", bloodTypes);
const ticketStatusEnum = pgEnum("ticket_status", ticketStatuses);

export const users = snakeCase.table("users", {
  id: integer().generatedAlwaysAsIdentity().primaryKey(),
  email: text().notNull().unique(),
  password: text().notNull(),
  role: userRoleEnum().notNull(),
  licenseId: integer().unique().references(() => licenses.id, {onDelete: "set null"})
});

export const licenses = snakeCase.table("licenses", {
  id: integer().generatedAlwaysAsIdentity().primaryKey(),
  number: text().notNull().unique(),
  type: licenseTypeEnum().notNull(),
  status: licenseStatusEnum().notNull(),
  issuanceDate: date().notNull(),
  expiryDate: date().notNull(),
  firstName: text().notNull(),
  middleName: text(),
  lastName: text().notNull(),
  sex: sexEnum().notNull(),
  dateOfBirth: date().notNull(),
  address: text().notNull(),
  eyeColor: eyeColorEnum().notNull(),
  height: integer().notNull(),
  weight: integer().notNull(),
  bloodType: bloodTypeEnum().notNull()
});

export const vehicles = snakeCase.table("vehicles", {
  id: integer().generatedAlwaysAsIdentity().primaryKey(),
  plateNumber: text().notNull().unique(),
  mvFileNumber: text().unique(),
  vin: text().unique(),
  issuanceDate: date().notNull(),
  expiryDate: date().notNull(),
  make: text().notNull(),
  model: text().notNull(),
  year: integer().notNull(),
  color: text().notNull(),
  licenseId: integer().references(() => licenses.id, {onDelete: "set null"})
});

export const violations = snakeCase.table("violations", {
  id: integer().generatedAlwaysAsIdentity().primaryKey(),
  name: text().notNull().unique(),
  isTiered: boolean().notNull(),
  initialFine: numeric().notNull(),
  secondFine: numeric(),
  thirdFine: numeric()
});

export const tickets = snakeCase.table("tickets", {
  id: integer().generatedAlwaysAsIdentity().primaryKey(),
  licenseId: integer().notNull().references(() => licenses.id, {onDelete: "restrict"}),
  referenceNumber: text().notNull().unique(),
  createdAt: timestamp({withTimezone: true}).notNull().defaultNow(),
  status: ticketStatusEnum().notNull(),
  dateOfIncident: date().notNull(),
  placeOfIncident: text().notNull(),
  officerNotes: text()
});

export const ticketViolations = snakeCase.table("ticket_violations", {
  ticketId: integer().notNull().references(() => tickets.id, {onDelete: "restrict"}),
  violationId: integer().notNull().references(() => violations.id, {onDelete: "restrict"}),
  fine: numeric().notNull()
}, (table) => [
  primaryKey({columns: [table.ticketId, table.violationId]})
]);