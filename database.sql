--
-- PostgreSQL database dump
--

-- Dumped from database version 14.1
-- Dumped by pg_dump version 14.1

-- Started on 2022-01-20 01:26:50

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

--
-- TOC entry 3372 (class 1262 OID 24576)
-- Name: mtcgdb; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE mtcgdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'C';


ALTER DATABASE mtcgdb OWNER TO postgres;

\connect mtcgdb

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

--
-- TOC entry 3 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 3373 (class 0 OID 0)
-- Dependencies: 3
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 24709)
-- Name: Auth_tokens; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Auth_tokens" (
    username character varying NOT NULL,
    token character varying NOT NULL
);


ALTER TABLE public."Auth_tokens" OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 24590)
-- Name: Cards; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Cards" (
    id character varying NOT NULL,
    name character varying NOT NULL,
    etype character varying NOT NULL,
    mtype character varying NOT NULL,
    dmg bigint NOT NULL
);


ALTER TABLE public."Cards" OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 24675)
-- Name: Cards_in_Decks; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Cards_in_Decks" (
    username character varying NOT NULL,
    card character varying NOT NULL
);


ALTER TABLE public."Cards_in_Decks" OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 24609)
-- Name: Packages; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Packages" (
    card1 character varying NOT NULL,
    card2 character varying NOT NULL,
    card3 character varying NOT NULL,
    card4 character varying NOT NULL,
    card5 character varying NOT NULL,
    id bigint NOT NULL
);


ALTER TABLE public."Packages" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 24779)
-- Name: Packages_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Packages" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Packages_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 211 (class 1259 OID 24597)
-- Name: Tradings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Tradings" (
    id character varying NOT NULL,
    type character varying NOT NULL,
    mindmg bigint NOT NULL,
    card character varying NOT NULL
);


ALTER TABLE public."Tradings" OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 24578)
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    username character varying NOT NULL,
    password character varying NOT NULL,
    name character varying NOT NULL,
    bio character varying NOT NULL,
    image character varying NOT NULL,
    coins bigint DEFAULT 20 NOT NULL,
    wins bigint DEFAULT 0 NOT NULL,
    losses bigint DEFAULT 0 NOT NULL,
    draws bigint DEFAULT 0 NOT NULL,
    elo bigint DEFAULT 100 NOT NULL
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 24692)
-- Name: Users_own_Cards; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users_own_Cards" (
    username character varying NOT NULL,
    card character varying NOT NULL
);


ALTER TABLE public."Users_own_Cards" OWNER TO postgres;

--
-- TOC entry 3216 (class 2606 OID 24715)
-- Name: Auth_tokens Auth_tokens_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Auth_tokens"
    ADD CONSTRAINT "Auth_tokens_pkey" PRIMARY KEY (token);


--
-- TOC entry 3196 (class 2606 OID 24596)
-- Name: Cards Cards_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_pkey" PRIMARY KEY (id);


--
-- TOC entry 3200 (class 2606 OID 24791)
-- Name: Packages Packages_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT "Packages_pkey" PRIMARY KEY (id);


--
-- TOC entry 3198 (class 2606 OID 24603)
-- Name: Tradings Tradings_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Tradings"
    ADD CONSTRAINT "Tradings_pkey" PRIMARY KEY (id);


--
-- TOC entry 3194 (class 2606 OID 24674)
-- Name: Users Users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY (username);


--
-- TOC entry 3202 (class 2606 OID 24722)
-- Name: Packages card1un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card1un UNIQUE (card1);


--
-- TOC entry 3204 (class 2606 OID 24724)
-- Name: Packages card2un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card2un UNIQUE (card2);


--
-- TOC entry 3206 (class 2606 OID 24726)
-- Name: Packages card3un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card3un UNIQUE (card3);


--
-- TOC entry 3208 (class 2606 OID 24728)
-- Name: Packages card4un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card4un UNIQUE (card4);


--
-- TOC entry 3210 (class 2606 OID 24730)
-- Name: Packages card5un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card5un UNIQUE (card5);


--
-- TOC entry 3214 (class 2606 OID 25025)
-- Name: Users_own_Cards card_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users_own_Cards"
    ADD CONSTRAINT card_pk PRIMARY KEY (card);


--
-- TOC entry 3212 (class 2606 OID 25027)
-- Name: Cards_in_Decks cards_in_deck_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cards_in_Decks"
    ADD CONSTRAINT cards_in_deck_pk PRIMARY KEY (card);


--
-- TOC entry 3217 (class 2606 OID 24604)
-- Name: Tradings card; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Tradings"
    ADD CONSTRAINT card FOREIGN KEY (card) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3224 (class 2606 OID 24687)
-- Name: Cards_in_Decks card; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cards_in_Decks"
    ADD CONSTRAINT card FOREIGN KEY (card) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3226 (class 2606 OID 24704)
-- Name: Users_own_Cards card; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users_own_Cards"
    ADD CONSTRAINT card FOREIGN KEY (card) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3221 (class 2606 OID 24639)
-- Name: Packages card1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card1 FOREIGN KEY (card1) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3222 (class 2606 OID 24644)
-- Name: Packages card2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card2 FOREIGN KEY (card2) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3218 (class 2606 OID 24624)
-- Name: Packages card3; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card3 FOREIGN KEY (card3) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3219 (class 2606 OID 24629)
-- Name: Packages card4; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card4 FOREIGN KEY (card4) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3220 (class 2606 OID 24634)
-- Name: Packages card5; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card5 FOREIGN KEY (card5) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3223 (class 2606 OID 24682)
-- Name: Cards_in_Decks user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cards_in_Decks"
    ADD CONSTRAINT "user" FOREIGN KEY (username) REFERENCES public."Users"(username) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3225 (class 2606 OID 24699)
-- Name: Users_own_Cards user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users_own_Cards"
    ADD CONSTRAINT "user" FOREIGN KEY (username) REFERENCES public."Users"(username) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3227 (class 2606 OID 24716)
-- Name: Auth_tokens user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Auth_tokens"
    ADD CONSTRAINT "user" FOREIGN KEY (username) REFERENCES public."Users"(username) ON UPDATE CASCADE ON DELETE CASCADE;


-- Completed on 2022-01-20 01:26:50

--
-- PostgreSQL database dump complete
--

