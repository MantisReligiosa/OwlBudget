export default function isNotEmpty(element) {
  return element && (element.length > 0 || (element + 0) > 0);
}
