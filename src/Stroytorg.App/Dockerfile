FROM node:lts as build
ARG APP_VERSION=1.0.0

WORKDIR /
ENV NX_DAEMON=false
COPY ./package.json ./package-lock.json /app/
RUN cd /app && npm set progress=false && npm install --force
COPY . /app
RUN cd /app && npm run build -- --prod