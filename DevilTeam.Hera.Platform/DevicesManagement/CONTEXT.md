# Devices Management

Owns the lifecycle of a smart-home **Device**: linking new hardware to an account,
listing it, renaming it, assigning it to a room, toggling its on/off state, and
unlinking it. This is the EP03 ("Gestión de Dispositivos IoT") context plus the
device-control slices of EP08 (`/api/devices`).

## Language

**Device**:
A single piece of smart-home hardware (light, plug, thermostat, camera, sensor)
linked to one account. The aggregate root of this context.
_Avoid_: Appliance, gadget, equipment, hardware (use "Device" everywhere).

**DeviceType**:
The kind of device — `Light`, `Plug`, `Thermostat`, `Camera`, `Sensor`, `Other`.
A **Camera** is a Device of type `Camera`; it is **not** a separate aggregate.
Live video / monitoring is a different context (EP04), out of scope here.

**DeviceCode**:
The pairing code printed on the hardware, supplied when linking (US11). Identifies
the physical unit; validated for shape at link time.
_Avoid_: Serial, pairing key, token.

**Room**:
A free-text zone label a device is assigned to (e.g. "Cocina", "Dormitorio Principal").
Used for grouping/filtering (US15). Plain string for now — not a value object.

**Connectivity** (`ConnectivityStatus`):
Whether the device is reachable: `Online` | `Offline` (US22). Independent of power.

**Power state** (`IsOn`):
Whether the device is switched on (US17/US38). Independent of connectivity.

**Owner** (`OwnerId`):
The account a device belongs to. Referenced **by id only** — no navigation property,
no FK to another context. Auth/ownership enforcement is deferred to IAM and is
currently unenforced (see Relationships).

## Relationships

- A **Device** belongs to exactly one **Owner** (account), referenced by `OwnerId`.
  No foreign key, no navigation — cross-context reference by id (same pattern as
  Profiles → IAM in the reference platform).
- **Connectivity** and **Power state** are two independent fields; a device can be
  `Online` + off, `Offline` + on (last-known), etc. (mirrors the frontend's
  `connectivityStatus` / `operationalStatus` split).

## Operations (granular, one command/query per story)

| Story | HTTP | Command / Query |
|-------|------|-----------------|
| US11 / US37 | `POST /api/v1/devices` | `CreateDeviceCommand` |
| US12 / US36 | `GET /api/v1/devices` | `GetAllDevicesQuery` |
| —           | `GET /api/v1/devices/{id}` | `GetDeviceByIdQuery` |
| US13        | `PATCH /api/v1/devices/{id}/name` | `UpdateDeviceNameCommand` |
| US15        | `PATCH /api/v1/devices/{id}/room` | `AssignDeviceToRoomCommand` |
| US17 / US38 | `PATCH /api/v1/devices/{id}/status` | `UpdateDeviceStatusCommand` |
| US14        | `DELETE /api/v1/devices/{id}` | `DeleteDeviceCommand` |

## Example dialogue

> **Dev:** "When the user links hardware with a code, is the Camera a different thing?"
> **Domain expert:** "No — a camera is just a **Device** whose **DeviceType** is `Camera`.
> Watching its feed is a separate screen (EP04); here we only manage the device itself."
> **Dev:** "And if the device is unplugged but the app says it's on?"
> **Domain expert:** "Those are two facts: **Connectivity** is `Offline`, **Power state**
> is still its last `IsOn` value. Don't collapse them."

## Flagged ambiguities

- "status" was overloaded (power vs. connectivity). Resolved: **`IsOn`** (power) and
  **`ConnectivityStatus`** (reachability) are separate fields.
- The frontend `device-iot-management` modelled **Camera** as its own entity/endpoint.
  Resolved for the backend: Camera is a **DeviceType**, not a separate aggregate.

## Out of scope (deferred)

- **Auth / ownership enforcement** (US38's 403): `OwnerId` is modelled but unenforced
  until IAM's JWT pipeline integrates.
- **Energy consumption** (EP06): lives in the `energy-analytics` context, not here.
- **Live camera feed / monitoring** (EP04): separate context.
