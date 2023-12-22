FROM node:lts as build

WORKDIR /
ENV NX_DAEMON=false
COPY ./package.json ./package-lock.json /app/
RUN cd /app && npm set progress=false && npm install
COPY . /app
RUN cd /app && npm run build -- --prod

FROM nginx:stable-alpine
COPY --from=build /app/dist/apps/stroytorg.app /usr/share/nginx/html
COPY ./deploy/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]