CREATE TABLE users
(
	id bigint generated always as identity primary key,
	first_name varchar(64) not null ,
	last_name varchar(64),
	username varchar(64),
	email text,
	email_confirmed boolean default false,
	image_path text,
	password_salt text,
	password_hash text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now() 
);

CREATE TABLE posts
(
	id bigint generated always as identity primary key,
	title text not null,
	description text,
	image_path text,
	user_id bigint references users(id),
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

CREATE TABLE coments
(
	id bigint generated always as identity primary key,
	post_id bigint references posts(id),
	user_id bigint references users(id),
	replay_coment_id bigint,
	coment_text text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

CREATE TABLE likes
(
	id bigint generated always as identity primary key,
	post_id bigint references posts(id),
	user_id bigint references users(id),
	is_liked boolean default false,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

