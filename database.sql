PGDMP     $    .    
             z            mtcgdb    14.1    14.1 (    0           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            1           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            2           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            3           1262    24576    mtcgdb    DATABASE     Q   CREATE DATABASE mtcgdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'C';
    DROP DATABASE mtcgdb;
                postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                postgres    false            4           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                   postgres    false    3            �            1259    24709    Auth_tokens    TABLE     u   CREATE TABLE public."Auth_tokens" (
    username character varying NOT NULL,
    token character varying NOT NULL
);
 !   DROP TABLE public."Auth_tokens";
       public         heap    postgres    false    3            �            1259    24590    Cards    TABLE     �   CREATE TABLE public."Cards" (
    id character varying NOT NULL,
    name character varying NOT NULL,
    etype character varying NOT NULL,
    mtype character varying NOT NULL,
    dmg bigint NOT NULL
);
    DROP TABLE public."Cards";
       public         heap    postgres    false    3            �            1259    24675    Cards_in_Decks    TABLE     w   CREATE TABLE public."Cards_in_Decks" (
    username character varying NOT NULL,
    card character varying NOT NULL
);
 $   DROP TABLE public."Cards_in_Decks";
       public         heap    postgres    false    3            �            1259    24609    Packages    TABLE     �   CREATE TABLE public."Packages" (
    card1 character varying NOT NULL,
    card2 character varying NOT NULL,
    card3 character varying NOT NULL,
    card4 character varying NOT NULL,
    card5 character varying NOT NULL,
    id bigint NOT NULL
);
    DROP TABLE public."Packages";
       public         heap    postgres    false    3            �            1259    24779    Packages_id_seq    SEQUENCE     �   ALTER TABLE public."Packages" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Packages_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    212    3            �            1259    49238    Sales    TABLE     �   CREATE TABLE public."Sales" (
    id character varying NOT NULL,
    card character varying NOT NULL,
    coins bigint NOT NULL
);
    DROP TABLE public."Sales";
       public         heap    postgres    false    3            �            1259    24597    Tradings    TABLE     �   CREATE TABLE public."Tradings" (
    id character varying NOT NULL,
    type character varying NOT NULL,
    mindmg bigint NOT NULL,
    card character varying NOT NULL
);
    DROP TABLE public."Tradings";
       public         heap    postgres    false    3            �            1259    24578    Users    TABLE     �  CREATE TABLE public."Users" (
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
    DROP TABLE public."Users";
       public         heap    postgres    false    3            �            1259    24692    Users_own_Cards    TABLE     x   CREATE TABLE public."Users_own_Cards" (
    username character varying NOT NULL,
    card character varying NOT NULL
);
 %   DROP TABLE public."Users_own_Cards";
       public         heap    postgres    false    3            �           2606    24715    Auth_tokens Auth_tokens_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public."Auth_tokens"
    ADD CONSTRAINT "Auth_tokens_pkey" PRIMARY KEY (token);
 J   ALTER TABLE ONLY public."Auth_tokens" DROP CONSTRAINT "Auth_tokens_pkey";
       public            postgres    false    215            �           2606    24596    Cards Cards_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_pkey" PRIMARY KEY (id);
 >   ALTER TABLE ONLY public."Cards" DROP CONSTRAINT "Cards_pkey";
       public            postgres    false    210            �           2606    24791    Packages Packages_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT "Packages_pkey" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT "Packages_pkey";
       public            postgres    false    212            �           2606    49244    Sales Sales_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public."Sales"
    ADD CONSTRAINT "Sales_pkey" PRIMARY KEY (id);
 >   ALTER TABLE ONLY public."Sales" DROP CONSTRAINT "Sales_pkey";
       public            postgres    false    217            �           2606    24603    Tradings Tradings_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Tradings"
    ADD CONSTRAINT "Tradings_pkey" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public."Tradings" DROP CONSTRAINT "Tradings_pkey";
       public            postgres    false    211            ~           2606    24674    Users Users_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY (username);
 >   ALTER TABLE ONLY public."Users" DROP CONSTRAINT "Users_pkey";
       public            postgres    false    209            �           2606    24722    Packages card1un 
   CONSTRAINT     N   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card1un UNIQUE (card1);
 <   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card1un;
       public            postgres    false    212            �           2606    24724    Packages card2un 
   CONSTRAINT     N   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card2un UNIQUE (card2);
 <   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card2un;
       public            postgres    false    212            �           2606    24726    Packages card3un 
   CONSTRAINT     N   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card3un UNIQUE (card3);
 <   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card3un;
       public            postgres    false    212            �           2606    24728    Packages card4un 
   CONSTRAINT     N   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card4un UNIQUE (card4);
 <   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card4un;
       public            postgres    false    212            �           2606    24730    Packages card5un 
   CONSTRAINT     N   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card5un UNIQUE (card5);
 <   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card5un;
       public            postgres    false    212            �           2606    25025    Users_own_Cards card_pk 
   CONSTRAINT     Y   ALTER TABLE ONLY public."Users_own_Cards"
    ADD CONSTRAINT card_pk PRIMARY KEY (card);
 C   ALTER TABLE ONLY public."Users_own_Cards" DROP CONSTRAINT card_pk;
       public            postgres    false    214            �           2606    25027    Cards_in_Decks cards_in_deck_pk 
   CONSTRAINT     a   ALTER TABLE ONLY public."Cards_in_Decks"
    ADD CONSTRAINT cards_in_deck_pk PRIMARY KEY (card);
 K   ALTER TABLE ONLY public."Cards_in_Decks" DROP CONSTRAINT cards_in_deck_pk;
       public            postgres    false    213            �           2606    24604    Tradings card    FK CONSTRAINT     �   ALTER TABLE ONLY public."Tradings"
    ADD CONSTRAINT card FOREIGN KEY (card) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 9   ALTER TABLE ONLY public."Tradings" DROP CONSTRAINT card;
       public          postgres    false    3200    211    210            �           2606    24687    Cards_in_Decks card    FK CONSTRAINT     �   ALTER TABLE ONLY public."Cards_in_Decks"
    ADD CONSTRAINT card FOREIGN KEY (card) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 ?   ALTER TABLE ONLY public."Cards_in_Decks" DROP CONSTRAINT card;
       public          postgres    false    213    210    3200            �           2606    24704    Users_own_Cards card    FK CONSTRAINT     �   ALTER TABLE ONLY public."Users_own_Cards"
    ADD CONSTRAINT card FOREIGN KEY (card) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 @   ALTER TABLE ONLY public."Users_own_Cards" DROP CONSTRAINT card;
       public          postgres    false    214    3200    210            �           2606    49245 
   Sales card    FK CONSTRAINT     �   ALTER TABLE ONLY public."Sales"
    ADD CONSTRAINT card FOREIGN KEY (card) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE;
 6   ALTER TABLE ONLY public."Sales" DROP CONSTRAINT card;
       public          postgres    false    217    210    3200            �           2606    24639    Packages card1    FK CONSTRAINT     �   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card1 FOREIGN KEY (card1) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 :   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card1;
       public          postgres    false    3200    210    212            �           2606    24644    Packages card2    FK CONSTRAINT     �   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card2 FOREIGN KEY (card2) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 :   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card2;
       public          postgres    false    3200    212    210            �           2606    24624    Packages card3    FK CONSTRAINT     �   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card3 FOREIGN KEY (card3) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 :   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card3;
       public          postgres    false    3200    210    212            �           2606    24629    Packages card4    FK CONSTRAINT     �   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card4 FOREIGN KEY (card4) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 :   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card4;
       public          postgres    false    212    210    3200            �           2606    24634    Packages card5    FK CONSTRAINT     �   ALTER TABLE ONLY public."Packages"
    ADD CONSTRAINT card5 FOREIGN KEY (card5) REFERENCES public."Cards"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 :   ALTER TABLE ONLY public."Packages" DROP CONSTRAINT card5;
       public          postgres    false    212    210    3200            �           2606    24682    Cards_in_Decks user    FK CONSTRAINT     �   ALTER TABLE ONLY public."Cards_in_Decks"
    ADD CONSTRAINT "user" FOREIGN KEY (username) REFERENCES public."Users"(username) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 A   ALTER TABLE ONLY public."Cards_in_Decks" DROP CONSTRAINT "user";
       public          postgres    false    213    209    3198            �           2606    24699    Users_own_Cards user    FK CONSTRAINT     �   ALTER TABLE ONLY public."Users_own_Cards"
    ADD CONSTRAINT "user" FOREIGN KEY (username) REFERENCES public."Users"(username) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 B   ALTER TABLE ONLY public."Users_own_Cards" DROP CONSTRAINT "user";
       public          postgres    false    214    3198    209            �           2606    24716    Auth_tokens user    FK CONSTRAINT     �   ALTER TABLE ONLY public."Auth_tokens"
    ADD CONSTRAINT "user" FOREIGN KEY (username) REFERENCES public."Users"(username) ON UPDATE CASCADE ON DELETE CASCADE;
 >   ALTER TABLE ONLY public."Auth_tokens" DROP CONSTRAINT "user";
       public          postgres    false    209    215    3198           