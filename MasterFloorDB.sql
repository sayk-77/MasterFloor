PGDMP  .    
                }            MasterFloorDB    16.4    17.0 3    !           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            "           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            #           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            $           1262    19309    MasterFloorDB    DATABASE     �   CREATE DATABASE "MasterFloorDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "MasterFloorDB";
                     postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                     pg_database_owner    false            %           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                        pg_database_owner    false    4            �            1259    19311    materialtype    TABLE     �   CREATE TABLE public.materialtype (
    id integer NOT NULL,
    name text NOT NULL,
    defect_percentage numeric(5,2) NOT NULL
);
     DROP TABLE public.materialtype;
       public         heap r       postgres    false    4            �            1259    19310    materialtype_id_seq    SEQUENCE     �   CREATE SEQUENCE public.materialtype_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.materialtype_id_seq;
       public               postgres    false    4    216            &           0    0    materialtype_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.materialtype_id_seq OWNED BY public.materialtype.id;
          public               postgres    false    215            �            1259    19367    partnerproducts    TABLE     �   CREATE TABLE public.partnerproducts (
    id integer NOT NULL,
    product_id integer,
    partner_id integer,
    quantity integer NOT NULL,
    sale_date date NOT NULL,
    CONSTRAINT partnerproducts_quantity_check CHECK ((quantity >= 0))
);
 #   DROP TABLE public.partnerproducts;
       public         heap r       postgres    false    4            �            1259    19366    partnerproducts_id_seq    SEQUENCE     �   CREATE SEQUENCE public.partnerproducts_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.partnerproducts_id_seq;
       public               postgres    false    224    4            '           0    0    partnerproducts_id_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.partnerproducts_id_seq OWNED BY public.partnerproducts.id;
          public               postgres    false    223            �            1259    19351    partners    TABLE     h  CREATE TABLE public.partners (
    id integer NOT NULL,
    partner_type text NOT NULL,
    name text NOT NULL,
    director text NOT NULL,
    email text NOT NULL,
    phone text NOT NULL,
    address text NOT NULL,
    inn character varying(12) NOT NULL,
    rating integer,
    CONSTRAINT partners_rating_check CHECK (((rating >= 1) AND (rating <= 10)))
);
    DROP TABLE public.partners;
       public         heap r       postgres    false    4            �            1259    19350    partners_id_seq    SEQUENCE     �   CREATE SEQUENCE public.partners_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.partners_id_seq;
       public               postgres    false    222    4            (           0    0    partners_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.partners_id_seq OWNED BY public.partners.id;
          public               postgres    false    221            �            1259    19333    products    TABLE     �   CREATE TABLE public.products (
    id integer NOT NULL,
    product_type_id integer,
    name text NOT NULL,
    article character varying(20) NOT NULL,
    min_partner_price numeric(10,2) NOT NULL
);
    DROP TABLE public.products;
       public         heap r       postgres    false    4            �            1259    19332    products_id_seq    SEQUENCE     �   CREATE SEQUENCE public.products_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.products_id_seq;
       public               postgres    false    220    4            )           0    0    products_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.products_id_seq OWNED BY public.products.id;
          public               postgres    false    219            �            1259    19322    producttype    TABLE     |   CREATE TABLE public.producttype (
    id integer NOT NULL,
    name text NOT NULL,
    coefficient numeric(5,2) NOT NULL
);
    DROP TABLE public.producttype;
       public         heap r       postgres    false    4            �            1259    19321    producttype_id_seq    SEQUENCE     �   CREATE SEQUENCE public.producttype_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.producttype_id_seq;
       public               postgres    false    218    4            *           0    0    producttype_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.producttype_id_seq OWNED BY public.producttype.id;
          public               postgres    false    217            d           2604    19314    materialtype id    DEFAULT     r   ALTER TABLE ONLY public.materialtype ALTER COLUMN id SET DEFAULT nextval('public.materialtype_id_seq'::regclass);
 >   ALTER TABLE public.materialtype ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    215    216    216            h           2604    19370    partnerproducts id    DEFAULT     x   ALTER TABLE ONLY public.partnerproducts ALTER COLUMN id SET DEFAULT nextval('public.partnerproducts_id_seq'::regclass);
 A   ALTER TABLE public.partnerproducts ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    223    224    224            g           2604    19354    partners id    DEFAULT     j   ALTER TABLE ONLY public.partners ALTER COLUMN id SET DEFAULT nextval('public.partners_id_seq'::regclass);
 :   ALTER TABLE public.partners ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    222    221    222            f           2604    19336    products id    DEFAULT     j   ALTER TABLE ONLY public.products ALTER COLUMN id SET DEFAULT nextval('public.products_id_seq'::regclass);
 :   ALTER TABLE public.products ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    219    220    220            e           2604    19325    producttype id    DEFAULT     p   ALTER TABLE ONLY public.producttype ALTER COLUMN id SET DEFAULT nextval('public.producttype_id_seq'::regclass);
 =   ALTER TABLE public.producttype ALTER COLUMN id DROP DEFAULT;
       public               postgres    false    217    218    218                      0    19311    materialtype 
   TABLE DATA           C   COPY public.materialtype (id, name, defect_percentage) FROM stdin;
    public               postgres    false    216   r;                 0    19367    partnerproducts 
   TABLE DATA           Z   COPY public.partnerproducts (id, product_id, partner_id, quantity, sale_date) FROM stdin;
    public               postgres    false    224   �;                 0    19351    partners 
   TABLE DATA           h   COPY public.partners (id, partner_type, name, director, email, phone, address, inn, rating) FROM stdin;
    public               postgres    false    222   �<                 0    19333    products 
   TABLE DATA           Y   COPY public.products (id, product_type_id, name, article, min_partner_price) FROM stdin;
    public               postgres    false    220   ?                 0    19322    producttype 
   TABLE DATA           <   COPY public.producttype (id, name, coefficient) FROM stdin;
    public               postgres    false    218   B@       +           0    0    materialtype_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.materialtype_id_seq', 5, true);
          public               postgres    false    215            ,           0    0    partnerproducts_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.partnerproducts_id_seq', 16, true);
          public               postgres    false    223            -           0    0    partners_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.partners_id_seq', 5, true);
          public               postgres    false    221            .           0    0    products_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.products_id_seq', 5, true);
          public               postgres    false    219            /           0    0    producttype_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.producttype_id_seq', 4, true);
          public               postgres    false    217            l           2606    19320 "   materialtype materialtype_name_key 
   CONSTRAINT     ]   ALTER TABLE ONLY public.materialtype
    ADD CONSTRAINT materialtype_name_key UNIQUE (name);
 L   ALTER TABLE ONLY public.materialtype DROP CONSTRAINT materialtype_name_key;
       public                 postgres    false    216            n           2606    19318    materialtype materialtype_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.materialtype
    ADD CONSTRAINT materialtype_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.materialtype DROP CONSTRAINT materialtype_pkey;
       public                 postgres    false    216            �           2606    19373 $   partnerproducts partnerproducts_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public.partnerproducts
    ADD CONSTRAINT partnerproducts_pkey PRIMARY KEY (id);
 N   ALTER TABLE ONLY public.partnerproducts DROP CONSTRAINT partnerproducts_pkey;
       public                 postgres    false    224            z           2606    19363    partners partners_email_key 
   CONSTRAINT     W   ALTER TABLE ONLY public.partners
    ADD CONSTRAINT partners_email_key UNIQUE (email);
 E   ALTER TABLE ONLY public.partners DROP CONSTRAINT partners_email_key;
       public                 postgres    false    222            |           2606    19365    partners partners_inn_key 
   CONSTRAINT     S   ALTER TABLE ONLY public.partners
    ADD CONSTRAINT partners_inn_key UNIQUE (inn);
 C   ALTER TABLE ONLY public.partners DROP CONSTRAINT partners_inn_key;
       public                 postgres    false    222            ~           2606    19361    partners partners_name_key 
   CONSTRAINT     U   ALTER TABLE ONLY public.partners
    ADD CONSTRAINT partners_name_key UNIQUE (name);
 D   ALTER TABLE ONLY public.partners DROP CONSTRAINT partners_name_key;
       public                 postgres    false    222            �           2606    19359    partners partners_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.partners
    ADD CONSTRAINT partners_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.partners DROP CONSTRAINT partners_pkey;
       public                 postgres    false    222            t           2606    19344    products products_article_key 
   CONSTRAINT     [   ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_article_key UNIQUE (article);
 G   ALTER TABLE ONLY public.products DROP CONSTRAINT products_article_key;
       public                 postgres    false    220            v           2606    19342    products products_name_key 
   CONSTRAINT     U   ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_name_key UNIQUE (name);
 D   ALTER TABLE ONLY public.products DROP CONSTRAINT products_name_key;
       public                 postgres    false    220            x           2606    19340    products products_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.products DROP CONSTRAINT products_pkey;
       public                 postgres    false    220            p           2606    19331     producttype producttype_name_key 
   CONSTRAINT     [   ALTER TABLE ONLY public.producttype
    ADD CONSTRAINT producttype_name_key UNIQUE (name);
 J   ALTER TABLE ONLY public.producttype DROP CONSTRAINT producttype_name_key;
       public                 postgres    false    218            r           2606    19329    producttype producttype_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.producttype
    ADD CONSTRAINT producttype_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.producttype DROP CONSTRAINT producttype_pkey;
       public                 postgres    false    218            �           2606    19379 /   partnerproducts partnerproducts_partner_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.partnerproducts
    ADD CONSTRAINT partnerproducts_partner_id_fkey FOREIGN KEY (partner_id) REFERENCES public.partners(id);
 Y   ALTER TABLE ONLY public.partnerproducts DROP CONSTRAINT partnerproducts_partner_id_fkey;
       public               postgres    false    222    224    4736            �           2606    19374 /   partnerproducts partnerproducts_product_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.partnerproducts
    ADD CONSTRAINT partnerproducts_product_id_fkey FOREIGN KEY (product_id) REFERENCES public.products(id);
 Y   ALTER TABLE ONLY public.partnerproducts DROP CONSTRAINT partnerproducts_product_id_fkey;
       public               postgres    false    224    220    4728            �           2606    19345 &   products products_product_type_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_product_type_id_fkey FOREIGN KEY (product_type_id) REFERENCES public.producttype(id);
 P   ALTER TABLE ONLY public.products DROP CONSTRAINT products_product_type_id_fkey;
       public               postgres    false    218    4722    220               V   x�3估��.칰�bӅ���v_ؠ`�i�gh�e�G�P��)�1%�@%F\&x�� ���r��Qb
Tbl����� �ah         �   x�U��!C�v/�X�n/鿎�l���&ē�Te�-���ڒ��'��͛u�m���#G��nԑ����I/+����
LBK���6�NuF·�f�& H�W����-G�\�ڳSұ]�y��UO/n��uG�]�3�����1�  ~�d�����Xϥ�}����Y;�         g  x��S�n�@]_1����ҍ�z%��ׁ��"H!��Vm��T��	��;�s�@hW!������%�M�Z�%��JIw~�OhE�ӂ�~"�+����wLi�7�~�st����B�Ȏ��A�}�����+����V�����Ji�lǢ�t䢦�o���y���T���{��~��&h{�Uh�K���*�ҟҲ%�;�1��hJ��e�Sq*�t�?A7h8���K���bMx&�CQȊ���;�q&���|���"PI�8Pqm'B�����ԟ��A��Z���ܡw�`�x���m��g�O���+I����;7�O�C��e��A�	㱗�����ϡ��0�;������L:QZj�6ZF�PI�*&�1�bҜ�܇�n*���?d7&����Z	kmd f�Z�3��YA���
h�g���5�}M���9,6���e�%�u8?̆y�����^��6M��P@K� +��I۬��F��{q���k���d`�'h��M�0�M\,\�mO�U�k?W�q��gs�n!�+������0?�٩oB��-��̌&J�@_9���g^z�s����,�Do�h���q�Z8�5JY�*j�i5��� ��         &  x�u�MN�0���)|,�?ؾ�I
��ذ@b��}STC�p��xv�
$�ȑ3��}�q��7�sKGJyE#�O�v4��^��Dc^ȉl� h�i���T����
��&8f���Qq]P/0}�8��`������!� ���k֊1Tm�za���d�ܰ��+"�W��ڡ�!?�MW�-�ڠ1��.w�d��A���(���br[n�(�;*�$�"ߕ���#���L�Jچcr�b�O�V2���ui�NxGQ����(��/��T��5�Y����Fr� Q(�         v   x�e���0�wUP�	c���y�� т�Bര�QV��u����
N�P|��b�A�2�>��~ͽ���@�l!kl,&��˿�,&M��4<�5���'�q��J���h���Q�     