ALTER TABLE codecoolers ADD PRIMARY KEY (id);
ALTER TABLE cities ADD PRIMARY KEY (id);
ALTER TABLE schools ADD PRIMARY KEY (id);

ALTER TABLE schools 
    ADD CONSTRAINT schools_city_fk 
    FOREIGN KEY (city_id)
    REFERENCES cities(id);

ALTER TABLE codecoolers 
    ADD CONSTRAINT codecoolers_birth_city_fk
    FOREIGN KEY (birth_city_id)
    REFERENCES cities(id);

ALTER TABLE codecoolers_schools
    ADD CONSTRAINT codecoolers_schools_codecooler_fk
    FOREIGN KEY (codecooler_id)
    REFERENCES codecoolers(id);

ALTER TABLE codecoolers_schools
    ADD CONSTRAINT codecoolers_schools_school_fk
    FOREIGN KEY (school_id)
    REFERENCES schools(id);
