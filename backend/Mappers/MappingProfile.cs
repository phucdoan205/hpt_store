using AutoMapper;
using backend.DTOs.Products;
using backend.DTOs.Orders;
using backend.Models.Products;
using backend.Models.Orders;
using backend.DTOs.Users;
using backend.Models.Users;
using backend.DTOs.Auth;

namespace backend.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // PRODUCTS
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<ProductVariant, ProductVariantDto>();
            CreateMap<ProductImage, ProductImageDto>();

            // ORDERS
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<OrderDetail, OrderDetailDto>();

            // USERS
            CreateMap<User, UserDto>();
            CreateMap<CreateStaffDto, User>();
        }
    }
}