FROM node:lts as build
ARG APP_VERSION=1.0.0

WORKDIR /
ENV NX_DAEMON=false
COPY ./package.json ./package-lock.json /app/
RUN cd /app && npm set progress=false && npm install --force
COPY . /app
RUN cd /app && npm run build -- --prod
RUN sed -Ei -e "s/__APP_VERSION__/${APP_VERSION}/" /app/dist/apps/stroytorg.app/browser/main.js

FROM nginx:stable-alpine
COPY --from=build /app/dist/apps/stroytorg.app/browser /usr/share/nginx/html
COPY ./deploy/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]