\c TaskWorkService

-- Seed test work_submissions and user_xp
-- Task IDs match the hardcoded IDs in TaskReviewSeeder (ReviewService)
-- Run ONCE after the schema is created and statuses are seeded

DO $$
DECLARE
  v_pending   UUID;
  v_approved  UUID;
  v_rejected  UUID;
  v_inreview  UUID;
  v_completed UUID;

  task_testing   UUID := 'b3e52d67-aef5-443e-8023-eaa42ddc2b3a';
  task_sensor    UUID := 'ec6ddafc-3424-4131-b14c-dbb2a8c69327';
  task_bandwidth UUID := '32e261e0-c061-4d36-967c-52e3b7fa17a6';
  task_system    UUID := 'f0b25a52-0a98-468d-aa5c-2ea93f4f5667';
  task_program   UUID := 'dd6d50e5-4b28-4e69-8ab1-9dc621a347d5';
  task_bus       UUID := '2ff5acd4-5a25-453a-86ae-52c342c9e7e7';
  task_array     UUID := 'd4e232b0-7ff9-4451-a21c-c549ae2e06f2';
  task_matrix    UUID := '7ff5f10b-678d-4182-9fb5-6826479fc887';

  -- Test student user IDs (these should match real Keycloak user IDs if possible)
  u1 UUID := 'a1b2c3d4-1111-1111-1111-000000000001';
  u2 UUID := 'a1b2c3d4-2222-2222-2222-000000000002';
  u3 UUID := 'a1b2c3d4-3333-3333-3333-000000000003';
  u4 UUID := 'a1b2c3d4-4444-4444-4444-000000000004';
  u5 UUID := 'a1b2c3d4-5555-5555-5555-000000000005';
  u6 UUID := 'a1b2c3d4-6666-6666-6666-000000000006';

  sub_id UUID;
BEGIN
  IF (SELECT COUNT(*) FROM work_submissions) > 0 THEN
    RAISE NOTICE 'work_submissions already seeded, skipping.';
    RETURN;
  END IF;

  SELECT id INTO v_pending   FROM work_submission_statuses WHERE name = 'Pending';
  SELECT id INTO v_approved  FROM work_submission_statuses WHERE name = 'Approved';
  SELECT id INTO v_rejected  FROM work_submission_statuses WHERE name = 'Rejected';
  SELECT id INTO v_inreview  FROM work_submission_statuses WHERE name = 'InReview';
  SELECT id INTO v_completed FROM work_submission_statuses WHERE name = 'Completed';

  -- ── Testing Async Patterns (8 submissions) ─────────────────────────────
  INSERT INTO work_submissions (taskid,taskname,userid,userfirstname,userlastname,statusid,xpreward,submissiondate) VALUES
    (task_testing,'Testing Async Patterns',u1,'Alice','King',   v_approved, 150, NOW()-'30 days'::interval),
    (task_testing,'Testing Async Patterns',u2,'Bob','Marsh',    v_approved, 150, NOW()-'28 days'::interval),
    (task_testing,'Testing Async Patterns',u3,'Leo','Parker',   v_rejected, 150, NOW()-'25 days'::interval),
    (task_testing,'Testing Async Patterns',u4,'Ella','Stone',   v_completed,150, NOW()-'22 days'::interval),
    (task_testing,'Testing Async Patterns',u5,'Ryan','Foster',  v_inreview, 150, NOW()-'20 days'::interval),
    (task_testing,'Testing Async Patterns',u6,'Chloe','Adams',  v_approved, 150, NOW()-'18 days'::interval),
    (task_testing,'Testing Async Patterns',u2,'Bob','Marsh',    v_pending,  150, NOW()-'10 days'::interval),
    (task_testing,'Testing Async Patterns',u3,'Leo','Parker',   v_completed,150, NOW()-'5 days'::interval);

  -- ── Input Sensor Integration (7 submissions) ───────────────────────────
  INSERT INTO work_submissions (taskid,taskname,userid,userfirstname,userlastname,statusid,xpreward,submissiondate) VALUES
    (task_sensor,'Input Sensor Integration',u1,'Alice','King',  v_rejected, 120, NOW()-'29 days'::interval),
    (task_sensor,'Input Sensor Integration',u4,'Ella','Stone',  v_approved, 120, NOW()-'26 days'::interval),
    (task_sensor,'Input Sensor Integration',u5,'Ryan','Foster', v_approved, 120, NOW()-'23 days'::interval),
    (task_sensor,'Input Sensor Integration',u6,'Chloe','Adams', v_pending,  120, NOW()-'19 days'::interval),
    (task_sensor,'Input Sensor Integration',u2,'Bob','Marsh',   v_completed,120, NOW()-'15 days'::interval),
    (task_sensor,'Input Sensor Integration',u3,'Leo','Parker',  v_inreview, 120, NOW()-'8 days'::interval),
    (task_sensor,'Input Sensor Integration',u1,'Alice','King',  v_approved, 120, NOW()-'3 days'::interval);

  -- ── Transmit Bandwidth Data (8 submissions) ────────────────────────────
  INSERT INTO work_submissions (taskid,taskname,userid,userfirstname,userlastname,statusid,xpreward,submissiondate) VALUES
    (task_bandwidth,'Transmit Bandwidth Data',u3,'Leo','Parker',   v_approved, 180, NOW()-'27 days'::interval),
    (task_bandwidth,'Transmit Bandwidth Data',u5,'Ryan','Foster',  v_rejected, 180, NOW()-'24 days'::interval),
    (task_bandwidth,'Transmit Bandwidth Data',u6,'Chloe','Adams',  v_approved, 180, NOW()-'21 days'::interval),
    (task_bandwidth,'Transmit Bandwidth Data',u1,'Alice','King',   v_completed,180, NOW()-'17 days'::interval),
    (task_bandwidth,'Transmit Bandwidth Data',u2,'Bob','Marsh',    v_inreview, 180, NOW()-'14 days'::interval),
    (task_bandwidth,'Transmit Bandwidth Data',u4,'Ella','Stone',   v_approved, 180, NOW()-'11 days'::interval),
    (task_bandwidth,'Transmit Bandwidth Data',u5,'Ryan','Foster',  v_pending,  180, NOW()-'7 days'::interval),
    (task_bandwidth,'Transmit Bandwidth Data',u6,'Chloe','Adams',  v_completed,180, NOW()-'2 days'::interval);

  -- ── Transmit System Protocol (9 submissions) ───────────────────────────
  INSERT INTO work_submissions (taskid,taskname,userid,userfirstname,userlastname,statusid,xpreward,submissiondate) VALUES
    (task_system,'Transmit System Protocol',u2,'Bob','Marsh',    v_approved, 200, NOW()-'35 days'::interval),
    (task_system,'Transmit System Protocol',u4,'Ella','Stone',   v_approved, 200, NOW()-'32 days'::interval),
    (task_system,'Transmit System Protocol',u1,'Alice','King',   v_rejected, 200, NOW()-'29 days'::interval),
    (task_system,'Transmit System Protocol',u3,'Leo','Parker',   v_completed,200, NOW()-'26 days'::interval),
    (task_system,'Transmit System Protocol',u5,'Ryan','Foster',  v_approved, 200, NOW()-'22 days'::interval),
    (task_system,'Transmit System Protocol',u6,'Chloe','Adams',  v_inreview, 200, NOW()-'18 days'::interval),
    (task_system,'Transmit System Protocol',u1,'Alice','King',   v_approved, 200, NOW()-'13 days'::interval),
    (task_system,'Transmit System Protocol',u2,'Bob','Marsh',    v_pending,  200, NOW()-'9 days'::interval),
    (task_system,'Transmit System Protocol',u3,'Leo','Parker',   v_completed,200, NOW()-'4 days'::interval);

  -- ── Parse Program Output (7 submissions) ──────────────────────────────
  INSERT INTO work_submissions (taskid,taskname,userid,userfirstname,userlastname,statusid,xpreward,submissiondate) VALUES
    (task_program,'Parse Program Output',u4,'Ella','Stone',   v_approved, 130, NOW()-'31 days'::interval),
    (task_program,'Parse Program Output',u6,'Chloe','Adams',  v_rejected, 130, NOW()-'27 days'::interval),
    (task_program,'Parse Program Output',u1,'Alice','King',   v_completed,130, NOW()-'23 days'::interval),
    (task_program,'Parse Program Output',u2,'Bob','Marsh',    v_approved, 130, NOW()-'19 days'::interval),
    (task_program,'Parse Program Output',u5,'Ryan','Foster',  v_inreview, 130, NOW()-'14 days'::interval),
    (task_program,'Parse Program Output',u3,'Leo','Parker',   v_approved, 130, NOW()-'9 days'::interval),
    (task_program,'Parse Program Output',u4,'Ella','Stone',   v_pending,  130, NOW()-'3 days'::interval);

  -- ── Quantify Bus Metrics (8 submissions) ──────────────────────────────
  INSERT INTO work_submissions (taskid,taskname,userid,userfirstname,userlastname,statusid,xpreward,submissiondate) VALUES
    (task_bus,'Quantify Bus Metrics',u5,'Ryan','Foster',  v_completed,160, NOW()-'33 days'::interval),
    (task_bus,'Quantify Bus Metrics',u3,'Leo','Parker',   v_approved, 160, NOW()-'29 days'::interval),
    (task_bus,'Quantify Bus Metrics',u6,'Chloe','Adams',  v_rejected, 160, NOW()-'25 days'::interval),
    (task_bus,'Quantify Bus Metrics',u1,'Alice','King',   v_approved, 160, NOW()-'21 days'::interval),
    (task_bus,'Quantify Bus Metrics',u2,'Bob','Marsh',    v_inreview, 160, NOW()-'16 days'::interval),
    (task_bus,'Quantify Bus Metrics',u4,'Ella','Stone',   v_approved, 160, NOW()-'11 days'::interval),
    (task_bus,'Quantify Bus Metrics',u5,'Ryan','Foster',  v_pending,  160, NOW()-'6 days'::interval),
    (task_bus,'Quantify Bus Metrics',u6,'Chloe','Adams',  v_completed,160, NOW()-'1 days'::interval);

  -- ── Compress Array Structure (7 submissions) ──────────────────────────
  INSERT INTO work_submissions (taskid,taskname,userid,userfirstname,userlastname,statusid,xpreward,submissiondate) VALUES
    (task_array,'Compress Array Structure',u2,'Bob','Marsh',    v_approved, 140, NOW()-'28 days'::interval),
    (task_array,'Compress Array Structure',u4,'Ella','Stone',   v_completed,140, NOW()-'24 days'::interval),
    (task_array,'Compress Array Structure',u1,'Alice','King',   v_rejected, 140, NOW()-'20 days'::interval),
    (task_array,'Compress Array Structure',u3,'Leo','Parker',   v_approved, 140, NOW()-'16 days'::interval),
    (task_array,'Compress Array Structure',u6,'Chloe','Adams',  v_inreview, 140, NOW()-'12 days'::interval),
    (task_array,'Compress Array Structure',u5,'Ryan','Foster',  v_approved, 140, NOW()-'7 days'::interval),
    (task_array,'Compress Array Structure',u2,'Bob','Marsh',    v_pending,  140, NOW()-'2 days'::interval);

  -- ── Reboot Matrix State (8 submissions) ───────────────────────────────
  INSERT INTO work_submissions (taskid,taskname,userid,userfirstname,userlastname,statusid,xpreward,submissiondate) VALUES
    (task_matrix,'Reboot Matrix State',u3,'Leo','Parker',   v_approved, 170, NOW()-'34 days'::interval),
    (task_matrix,'Reboot Matrix State',u5,'Ryan','Foster',  v_approved, 170, NOW()-'30 days'::interval),
    (task_matrix,'Reboot Matrix State',u1,'Alice','King',   v_completed,170, NOW()-'26 days'::interval),
    (task_matrix,'Reboot Matrix State',u6,'Chloe','Adams',  v_rejected, 170, NOW()-'22 days'::interval),
    (task_matrix,'Reboot Matrix State',u2,'Bob','Marsh',    v_approved, 170, NOW()-'17 days'::interval),
    (task_matrix,'Reboot Matrix State',u4,'Ella','Stone',   v_inreview, 170, NOW()-'12 days'::interval),
    (task_matrix,'Reboot Matrix State',u3,'Leo','Parker',   v_pending,  170, NOW()-'6 days'::interval),
    (task_matrix,'Reboot Matrix State',u5,'Ryan','Foster',  v_completed,170, NOW()-'1 days'::interval);

  RAISE NOTICE 'work_submissions seeded successfully.';

  -- ── user_xp for Approved / Completed submissions ───────────────────────
  IF (SELECT COUNT(*) FROM user_xp) > 0 THEN
    RAISE NOTICE 'user_xp already seeded, skipping.';
    RETURN;
  END IF;

  INSERT INTO user_xp (userid,taskid,xpamount,earnedat) VALUES
    -- Alice King
    (u1,task_testing,  150, NOW()-'30 days'::interval),
    (u1,task_bandwidth,180, NOW()-'17 days'::interval),
    (u1,task_system,   200, NOW()-'13 days'::interval),
    (u1,task_program,  130, NOW()-'23 days'::interval),
    (u1,task_bus,      160, NOW()-'21 days'::interval),
    (u1,task_matrix,   170, NOW()-'26 days'::interval),
    -- Bob Marsh
    (u2,task_testing,  150, NOW()-'28 days'::interval),
    (u2,task_sensor,   120, NOW()-'15 days'::interval),
    (u2,task_system,   200, NOW()-'35 days'::interval),
    (u2,task_program,  130, NOW()-'19 days'::interval),
    (u2,task_array,    140, NOW()-'28 days'::interval),
    (u2,task_matrix,   170, NOW()-'17 days'::interval),
    -- Leo Parker
    (u3,task_testing,  150, NOW()-'5 days'::interval),
    (u3,task_bandwidth,180, NOW()-'27 days'::interval),
    (u3,task_system,   200, NOW()-'26 days'::interval),
    (u3,task_program,  130, NOW()-'9 days'::interval),
    (u3,task_bus,      160, NOW()-'29 days'::interval),
    (u3,task_array,    140, NOW()-'16 days'::interval),
    (u3,task_matrix,   170, NOW()-'34 days'::interval),
    -- Ella Stone
    (u4,task_testing,  150, NOW()-'22 days'::interval),
    (u4,task_sensor,   120, NOW()-'26 days'::interval),
    (u4,task_system,   200, NOW()-'32 days'::interval),
    (u4,task_program,  130, NOW()-'31 days'::interval),
    (u4,task_bus,      160, NOW()-'11 days'::interval),
    (u4,task_array,    140, NOW()-'24 days'::interval),
    -- Ryan Foster
    (u5,task_sensor,   120, NOW()-'23 days'::interval),
    (u5,task_system,   200, NOW()-'22 days'::interval),
    (u5,task_bus,      160, NOW()-'33 days'::interval),
    (u5,task_array,    140, NOW()-'7 days'::interval),
    (u5,task_matrix,   170, NOW()-'30 days'::interval),
    (u5,task_matrix,   170, NOW()-'1 days'::interval),
    -- Chloe Adams
    (u6,task_testing,  150, NOW()-'18 days'::interval),
    (u6,task_sensor,   120, NOW()-'3 days'::interval),
    (u6,task_bandwidth,180, NOW()-'21 days'::interval),
    (u6,task_bandwidth,180, NOW()-'2 days'::interval),
    (u6,task_bus,      160, NOW()-'1 days'::interval),
    (u6,task_matrix,   170, NOW()-'1 days'::interval); -- wait, same as above, different task

  RAISE NOTICE 'user_xp seeded successfully.';
END;
$$;
