--
-- PostgreSQL database dump
--

-- Dumped from database version 12.1
-- Dumped by pg_dump version 12.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    login character varying NOT NULL,
    password bytea NOT NULL,
    salt bytea NOT NULL,
    creation_date date NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.users (id, login, password, salt, creation_date) VALUES (1, 'user1', '\xef90873327dca30a4e097ada398e14fba170a2e062f5cf23c2548a33e34c074aa0ad92dfb696832e6a12d55a1dd3bab0ba3012dc84bee156d83cc137358c7478', '\x221505b6e42ea3fbc139f2bbb268f2d0', '2020-02-02');
INSERT INTO public.users (id, login, password, salt, creation_date) VALUES (2, 'user2', '\x8100324e605dce0d55a35a9d7ee0604f39686ab7764ba3c30c70a923b77e2db84798b44015ef771d85ec8fa36d4687155ba0dbd2598223e15606bdeed99b17db', '\x30610d58d9246a6f7a3d0109ef52b86b', '2020-02-02');
INSERT INTO public.users (id, login, password, salt, creation_date) VALUES (3, 'user3', '\x499ba1e251f5c720d82f12ba4a3d6e5dd36acd28bab13aa07d53b881f2215ec824558aedf3af51b7d47458241b3b32a0d9884ec0a7616d343b17a5ed8fe28ac9', '\x854234f752826922ef1142ed226be470', '2020-02-02');
INSERT INTO public.users (id, login, password, salt, creation_date) VALUES (4, 'fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567', '\x45c6313ef73169fb3c8607e2b1b20bfed43949ff4004238b2ec95a14db264baaade1405058ae71cc60a6f399ec593ad9acffef16c0b4062a287ccf0a33943afb', '\x96bbc29663f2a54a6736fec22dd90a82', '2020-02-03');
INSERT INTO public.users (id, login, password, salt, creation_date) VALUES (5, 'fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567fggghgg +567 1', '\x9f8e5e3907cd6401d11ab33e6f838b4af3b8afe45b4debb2f85efaadca57d3f78bc9d9aaf48b88132281d748d12db9515bab0cf4fc763ddfdfa7f65caaaeb124', '\xc4506e2e96d6dd2609fcd5ed1f8ff000', '2020-02-03');
INSERT INTO public.users (id, login, password, salt, creation_date) VALUES (6, '123', '\xd5694cea2d36865bfe62cf8f4cc3f403e45fc123020d04073030619bbd45a84ee01d1764273ffa36295ab1a72af68b7810c6c655bf6832b61e4baf68c6da10df', '\x7de0f112876795575850329e1902d2b2', '2020-02-03');
INSERT INTO public.users (id, login, password, salt, creation_date) VALUES (7, '1', '\x8b9f89a06991f0c7bceab5849f3a6e23c54f74e516c85eb105b00bcd65f0b23bc60a9a2ae39d020d369a4bc8914d360617642a38876f0876c7ce1019ca1f58be', '\xdcda06b13c476dbfde9b8d661db72bd7', '2020-02-03');
INSERT INTO public.users (id, login, password, salt, creation_date) VALUES (8, '1 _', '\xf1d9cfcb9d48040cd79ea86b083c4710354182a94707eba98b870d8f8e636d8d9f0af8764b6dcca3e8e50a2d51b8bd9cdac7b44f09741653c8eb8a654adf0e93', '\x45fd454de8f8fb5dbb16d546a43e0d06', '2020-02-03');


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 8, true);


--
-- Name: users users_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pk PRIMARY KEY (id);


--
-- Name: users_login_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX users_login_uindex ON public.users USING btree (login);


--
-- PostgreSQL database dump complete
--

